using StudioForge.Engine.Core;
using StudioForge.TotalMiner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectOSRS
{
    class ItemDictionary
    {
        public static ItemDictionary Instance;
        private Dictionary<string, Item> _dictionary;

        public ItemDictionary(string modPath, Item offset)
        {
            ItemDictionary.Instance = this;
            _dictionary = new Dictionary<string, Item>(StringComparer.OrdinalIgnoreCase);

            var itemDataPath = Path.Combine(new[] {FileSystem.RootPath, modPath, "ItemData.xml"});
            try
            {
                var itemData = Utils.Deserialize1<ModItemDataXML[]>(itemDataPath);
                foreach (ModItemDataXML item in itemData)
                {
                    _dictionary.Add(item.ItemID, offset++);
                }
                Logger.Log($"Loaded {_dictionary.Keys.Count} items from {itemDataPath}");
            } catch (Exception e)
            {
                Logger.LogErr($"Failed to load items from {itemDataPath}\n{e}");
            }
        }

        public Item Get(string itemID)
        {
            if (_dictionary.TryGetValue(itemID, out Item item)) {
                return item;
            } else
            {
                return Item.None;
            }
        }
    }
}
