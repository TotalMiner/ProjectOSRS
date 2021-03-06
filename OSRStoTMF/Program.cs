﻿using ImageMagick;
using Newtonsoft.Json;
using OSRStoTMF.OSRS;
using OSRStoTMF.Rules;
using OSRStoTMF.TotalMiner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OSRStoTMF
{
    class Program
    {
        private static List<Rule> rules;
        private static string currentPath;
        private static string itemsFailedPath;
        private static string itemsPath;
        private static string gifitemsPath;
        private static string outPath;
        private static OSRSItemSchema schema;
        private static WebClient client;
        private static bool imageCached;

        static void Main(string[] args)
        {
            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            itemsFailedPath = Path.Combine(currentPath, "items_failed");
            itemsPath = Path.Combine(currentPath, "items");
            gifitemsPath = Path.Combine(currentPath, "gifitems");
            outPath = Path.Combine(currentPath, "out");
            schema = JsonConvert.DeserializeObject<OSRSItemSchema>(File.ReadAllText(Path.Combine(currentPath, "items.json")));
            client = new WebClient();
            imageCached = File.Exists(Path.Combine(outPath, "tpi_32.png"));

            rules = new List<Rule>();
            rules.Add(new DefaultRule());

            CheckDirectoryExists(itemsFailedPath);
            CheckDirectoryExists(itemsPath);
            CheckDirectoryExists(outPath);

            int i = 1;
            if (!imageCached)
            {
                foreach (OSRSItem item in schema.Items.Values)
                {
                    bool downloaded = false;
                    try
                    {
                        if (!File.Exists(GetItemPathPng(item.Id)) && !File.Exists(Get404ItemPath(item.Id)))
                        {
                            DownloadItem(item.Id);
                            downloaded = true;
                        }
                        if (item.HasNoted && item.NotedId > 0 && !File.Exists(GetItemPathPng(item.NotedId)) && !File.Exists(Get404ItemPath(item.NotedId)))
                        {
                            DownloadItem(item.NotedId);
                        }
                    }
                    catch (WebException e)
                    {
                        int id = File.Exists(GetItemPathPng(item.Id)) ? item.NotedId : item.Id;
                        Console.WriteLine(Get404ItemPath(id));
                        Console.WriteLine(File.Exists(Get404ItemPath(id)));
                        Console.WriteLine($"Error downloading {id} ({i}/{schema.Items.Values.Count})");
                        Console.WriteLine(e);
                        Touch(Get404ItemPath(id));
                        downloaded = false;
                    }
                    finally
                    {
                        if (downloaded)
                        {
                            Console.WriteLine($"Downloaded {item.Id}.png ({i}/{schema.Items.Values.Count})");
                        }
                        else if (File.Exists(GetItemPathPng(item.Id)))
                        {
                            Console.WriteLine($"{item.Id}.png exists ({i}/{schema.Items.Values.Count})");
                        }
                    }
                    i++;
                }
            } else
            {
                Console.WriteLine("Spritesheet found, skipping scraping as it can be time consuming.");
            }

            Console.WriteLine("Done scraping!");

            MagickImageCollection magickImageCollection = new MagickImageCollection();
            List<ModItemDataXML> modItemList = new List<ModItemDataXML>();
            List<ModItemTypeDataXML> modItemTypeList = new List<ModItemTypeDataXML>();
            List<ItemXML> itemTextures32 = new List<ItemXML>();
            i = 1;
            int count = schema.Items.Count;
            foreach (OSRSItem item in schema.Items.Values)
            {
                if(File.Exists(GetItemPathPng(item.Id)))
                {
                    AddItem(item, modItemList, modItemTypeList, itemTextures32, magickImageCollection);
                    if(item.HasNoted && item.NotedId > 0 && File.Exists(GetItemPathPng(item.NotedId)))
                    {
                        AddItem(new OSRSItem
                        {
                            Name = $"{item.Name} (noted)",
                            Id = item.NotedId,
                            Description = "Swap this note at any bank for the equivalent item.",
                            CanStack = true
                        }, modItemList, modItemTypeList, itemTextures32, magickImageCollection);
                    }
                    Console.WriteLine($"Added {item.Id} ({i}/{count})");
                }
                i++;
            }

            var duplicates = modItemList.GroupBy(a => a.ItemID).Where(a => a.Count() > 1);
            Console.WriteLine($"Duplicate Count: {duplicates.Count()}");
            if(duplicates.Count() > 0)
            {
                foreach(IGrouping<string, ModItemDataXML> item in duplicates)
                {
                    Console.WriteLine(item.First().ItemID);
                }
                Console.ReadLine();
            }
            if (!imageCached)
            {
                int size = (int)Math.Ceiling(Math.Sqrt((double)itemTextures32.Count));
                Console.WriteLine($"Montage size is {size}x{size}");

                var montageSettings = new MontageSettings
                {
                    TileGeometry = new MagickGeometry(size, size),
                    Geometry = new MagickGeometry(32, 32),
                    BackgroundColor = new MagickColor(214, 147, 255, 0),
                };

                var spriteSheet = magickImageCollection.Montage(montageSettings);
                spriteSheet.Write(Path.Combine(outPath, "tpi_32.png"));
                Console.WriteLine($"magickImageCollection: {magickImageCollection.Count}");
                SerializeObject<ItemXML[]>(itemTextures32.ToArray(), Path.Combine(outPath, "ItemTextures32.xml"));
                Console.WriteLine($"itemTextures32: {itemTextures32.Count}");
            } else
            {
                Console.WriteLine("Spritesheet found, skipping stitching as it can be time consuming.");
            }
            SerializeObject<ModItemDataXML[]>(modItemList.ToArray(), Path.Combine(outPath, "ItemData.xml"));
            Console.WriteLine($"modItemList: {modItemList.Count}");
            SerializeObject<ModItemTypeDataXML[]>(modItemTypeList.ToArray(), Path.Combine(outPath, "ItemTypeData.xml"));
            Console.WriteLine($"modItemTypeList: {modItemTypeList.Count}");

            Console.ReadLine();
        }

        static void DownloadItem(int id)
        {
            try
            {
                client.DownloadFile($"https://assets.hexagondev.net/osrs/sprites/items/{id}", GetItemPathPng(id));
            } catch (WebException)
            {
                client.DownloadFile($"http://cdn.rsbuddy.com/items/{id}.png", GetItemPathGif(id));
                var img = new MagickImage(GetItemPathGif(id));
                img.Format = MagickFormat.Png;
                img.Write(GetItemPathPng(id));
                img.Dispose();
            }
        }

        static void AddItem(OSRSItem osrsItem, List<ModItemDataXML> itemDataList, List<ModItemTypeDataXML> itemTypeDataList, List<ItemXML> texturesList, MagickImageCollection imageCollection)
        {
            int dupes;
            if (itemDataList.Exists(e => e.ItemID.Equals(NormalizeItemName(osrsItem.Name), StringComparison.InvariantCultureIgnoreCase)))
            {
                dupes = itemDataList.Where(e => e.ItemID.StartsWith(NormalizeItemName(osrsItem.Name) + "_", StringComparison.InvariantCultureIgnoreCase)).Count() + 1;
            } else
            {
                dupes = 0;
            }
            string itemid;
            if (dupes > 0)
            {
                itemid = $"{NormalizeItemName(osrsItem.Name)}_{dupes + 1}";
            } else
            {
                itemid = NormalizeItemName(osrsItem.Name);
            }
            if (!imageCached)
            {
                var img = new MagickImage(GetItemPathPng(osrsItem.Id));
                img.Crop(0, 0, 32, 32);
                imageCollection.Add(img);
                texturesList.Add(new ItemXML { ItemID = itemid });
            }
            var itemData = new ModItemDataXML();
            var itemTypeData = new ModItemTypeDataXML();

            foreach(Rule rule in rules)
            {
                if (rule.ShouldTransform(itemid, osrsItem))
                {
                    itemData = rule.TransformItemData(itemid, osrsItem, itemData);
                    itemTypeData = rule.TransformItemTypeData(itemid, osrsItem, itemTypeData);
                }
            }

            itemDataList.Add(itemData);
            itemTypeDataList.Add(itemTypeData);
        }

        static string NormalizeItemName(string name)
        {
            return Regex.Replace(UppercaseFirst(name), @"[^A-Za-z0-9_]", "");
        }

        static string UppercaseFirst(string s)
        {
            char[] array = s.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        static string Get404ItemPath(int id)
        {
            return Path.Combine(itemsFailedPath, $"{id}.404");
        }

        static string GetItemPathPng(int id)
        {
            return Path.Combine(itemsPath, $"{id}.png");
        }

        static string GetItemPathGif(int id)
        {
            return Path.Combine(gifitemsPath, $"{id}.gif");
        }

        static void Touch(string fileName)
        {
            FileStream myFileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            myFileStream.Close();
            myFileStream.Dispose();
            File.SetLastWriteTimeUtc(fileName, DateTime.UtcNow);
        }

        static void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void CheckDirectoryExists(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
