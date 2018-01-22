using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class BronzeLgSet : Set
    {
        private Item BronzeFullHelm;
        private Item BronzePlatebody;
        private Item BronzePlatelegs;
        private Item BronzeKiteshield;
        private Item BronzeLgSetItem;

        public BronzeLgSet()
        {
            this.BronzeFullHelm = ItemDictionary.Instance.Get("BronzeFullHelm");
            this.BronzePlatebody = ItemDictionary.Instance.Get("BronzePlatebody");
            this.BronzePlatelegs = ItemDictionary.Instance.Get("BronzePlatelegs");
            this.BronzeKiteshield = ItemDictionary.Instance.Get("BronzeKiteshield");
            this.BronzeLgSetItem = ItemDictionary.Instance.Get("BronzeSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.BronzeFullHelm, 1),
                new InventoryItem(this.BronzePlatebody, 1),
                new InventoryItem(this.BronzePlatelegs, 1),
                new InventoryItem(this.BronzeKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.BronzeLgSetItem;
        }
    }
}
