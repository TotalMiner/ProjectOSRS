using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class SteelLgSet : Set
    {
        private Item SteelFullHelm;
        private Item SteelPlatebody;
        private Item SteelPlatelegs;
        private Item SteelKiteshield;
        private Item SteelLgSetItem;

        public SteelLgSet()
        {
            this.SteelFullHelm = ItemDictionary.Instance.Get("SteelFullHelm");
            this.SteelPlatebody = ItemDictionary.Instance.Get("SteelPlatebody");
            this.SteelPlatelegs = ItemDictionary.Instance.Get("SteelPlatelegs");
            this.SteelKiteshield = ItemDictionary.Instance.Get("SteelKiteshield");
            this.SteelLgSetItem = ItemDictionary.Instance.Get("SteelSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.SteelFullHelm, 1),
                new InventoryItem(this.SteelPlatebody, 1),
                new InventoryItem(this.SteelPlatelegs, 1),
                new InventoryItem(this.SteelKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.SteelLgSetItem;
        }
    }
}
