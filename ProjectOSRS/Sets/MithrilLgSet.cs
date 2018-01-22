using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class MithrilLgSet : Set
    {
        private Item MithrilFullHelm;
        private Item MithrilPlatebody;
        private Item MithrilPlatelegs;
        private Item MithrilKiteshield;
        private Item MithrilLgSetItem;

        public MithrilLgSet()
        {
            this.MithrilFullHelm = ItemDictionary.Instance.Get("MithrilFullHelm");
            this.MithrilPlatebody = ItemDictionary.Instance.Get("MithrilPlatebody");
            this.MithrilPlatelegs = ItemDictionary.Instance.Get("MithrilPlatelegs");
            this.MithrilKiteshield = ItemDictionary.Instance.Get("MithrilKiteshield");
            this.MithrilLgSetItem = ItemDictionary.Instance.Get("MithrilSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.MithrilFullHelm, 1),
                new InventoryItem(this.MithrilPlatebody, 1),
                new InventoryItem(this.MithrilPlatelegs, 1),
                new InventoryItem(this.MithrilKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.MithrilLgSetItem;
        }
    }
}
