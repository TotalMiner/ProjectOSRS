using ImageMagick;
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
        private static string scrapedPath;
        private static string itemsPath;
        private static string outPath;
        private static OSRSItemSchema schema;
        private static WebClient client;
        private static bool imageCached;

        static void Main(string[] args)
        {
            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            scrapedPath = Path.Combine(currentPath, "scraped");
            itemsPath = Path.Combine(currentPath, "items");
            outPath = Path.Combine(currentPath, "out");
            schema = JsonConvert.DeserializeObject<OSRSItemSchema>(File.ReadAllText(Path.Combine(currentPath, "items.json")));
            client = new WebClient();
            imageCached = File.Exists(Path.Combine(outPath, "tpi_32.png"));

            rules = new List<Rule>();
            rules.Add(new DefaultRule());

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
                            client.DownloadFile($"http://cdn.rsbuddy.com/items/{item.Id}.png", GetItemPathPng(item.Id));
                            //client.DownloadFile($"http://services.runescape.com/m=itemdb_oldschool/1513605640124_obj_sprite.gif?id={item.Id}", GetItemPathGif(item.Id));
                            downloaded = true;
                        }
                        if (HasNoted(item.Id, schema) && !File.Exists(GetNotedItemPath(item.Id)) && !File.Exists(Get404ItemPath(item.Id + 1)))
                        {
                            client.DownloadFile($"http://cdn.rsbuddy.com/items/{item.Id + 1}.png", GetItemPathPng(item.Id + 1));
                        }
                    }
                    catch (WebException e)
                    {
                        int offset = File.Exists(GetItemPathPng(item.Id)) ? 1 : 0;
                        int id = item.Id + offset;
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
                    if(HasNoted(item.Id, schema) && File.Exists(GetNotedItemPath(item.Id)))
                    {
                        AddItem(new OSRSItem
                        {
                            Name = $"{item.Name} (noted)",
                            Id = item.Id + 1,
                            Description = item.Description,
                            CanStack = true
                        }, modItemList, modItemTypeList, itemTextures32, magickImageCollection);
                    }
                    Console.WriteLine($"Added {item.Id} ({i}/{count})");
                }
                i++;
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

        static void AddItem(OSRSItem osrsItem, List<ModItemDataXML> itemDataList, List<ModItemTypeDataXML> itemTypeDataList, List<ItemXML> texturesList, MagickImageCollection imageCollection)
        {
            int dupes = itemDataList.Where(e => e.ItemID.StartsWith(NormalizeItemName(osrsItem.Name))).Count();
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
            return Path.Combine(scrapedPath, $"{id}.404");
        }

        static string GetItemPathPng(int id)
        {
            return Path.Combine(itemsPath, $"{id}.png");
        }

        static string GetNotedItemPath(int id)
        {
            return Path.Combine(itemsPath, $"{id + 1}.png");
        }

        static bool HasNoted(int id, OSRSItemSchema schema)
        {
            if (schema.Items.TryGetValue(id.ToString(), out OSRSItem item))
            {
                return !schema.Items.ContainsKey((item.Id + 1).ToString()) && !item.CanStack;
            } else
            {
                return false;
            }
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
                //Log exception here
            }
        }
    }
}
