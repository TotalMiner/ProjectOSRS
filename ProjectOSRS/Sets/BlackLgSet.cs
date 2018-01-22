using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class BlackLgSet : Set
    {
        private Item BlackFullHelm;
        private Item BlackPlatebody;
        private Item BlackPlatelegs;
        private Item BlackKiteshield;
        private Item BlackLgSetItem;

        public BlackLgSet()
        {
            this.BlackFullHelm = ItemDictionary.Instance.Get("BlackFullHelm");
            this.BlackPlatebody = ItemDictionary.Instance.Get("BlackPlatebody");
            this.BlackPlatelegs = ItemDictionary.Instance.Get("BlackPlatelegs");
            this.BlackKiteshield = ItemDictionary.Instance.Get("BlackKiteshield");
            this.BlackLgSetItem = ItemDictionary.Instance.Get("BlackSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.BlackFullHelm, 1),
                new InventoryItem(this.BlackPlatebody, 1),
                new InventoryItem(this.BlackPlatelegs, 1),
                new InventoryItem(this.BlackKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.BlackLgSetItem;
        }
    }
}
