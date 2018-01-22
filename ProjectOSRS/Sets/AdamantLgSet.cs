using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class AdamantLgSet : Set
    {
        private Item AdamantFullHelm;
        private Item AdamantPlatebody;
        private Item AdamantPlatelegs;
        private Item AdamantKiteshield;
        private Item AdamantLgSetItem;

        public AdamantLgSet()
        {
            this.AdamantFullHelm = ItemDictionary.Instance.Get("AdamantFullHelm");
            this.AdamantPlatebody = ItemDictionary.Instance.Get("AdamantPlatebody");
            this.AdamantPlatelegs = ItemDictionary.Instance.Get("AdamantPlatelegs");
            this.AdamantKiteshield = ItemDictionary.Instance.Get("AdamantKiteshield");
            this.AdamantLgSetItem = ItemDictionary.Instance.Get("AdamantSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.AdamantFullHelm, 1),
                new InventoryItem(this.AdamantPlatebody, 1),
                new InventoryItem(this.AdamantPlatelegs, 1),
                new InventoryItem(this.AdamantKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.AdamantLgSetItem;
        }
    }
}
