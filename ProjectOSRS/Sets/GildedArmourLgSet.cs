using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class GildedArmourLgSet : Set
    {
        private Item GildedFullHelm;
        private Item GildedPlatebody;
        private Item GildedPlatelegs;
        private Item GildedKiteshield;
        private Item GildedArmourLgSetItem;

        public GildedArmourLgSet()
        {
            this.GildedFullHelm = ItemDictionary.Instance.Get("GildedFullHelm");
            this.GildedPlatebody = ItemDictionary.Instance.Get("GildedPlatebody");
            this.GildedPlatelegs = ItemDictionary.Instance.Get("GildedPlatelegs");
            this.GildedKiteshield = ItemDictionary.Instance.Get("GildedKiteshield");
            this.GildedArmourLgSetItem = ItemDictionary.Instance.Get("GildedArmourSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.GildedFullHelm, 1),
                new InventoryItem(this.GildedPlatebody, 1),
                new InventoryItem(this.GildedPlatelegs, 1),
                new InventoryItem(this.GildedKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.GildedArmourLgSetItem;
        }
    }
}
