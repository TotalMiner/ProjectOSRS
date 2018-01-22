using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class IronLgSet : Set
    {
        private Item IronFullHelm;
        private Item IronPlatebody;
        private Item IronPlatelegs;
        private Item IronKiteshield;
        private Item IronLgSetItem;

        public IronLgSet()
        {
            this.IronFullHelm = ItemDictionary.Instance.Get("IronFullHelm");
            this.IronPlatebody = ItemDictionary.Instance.Get("IronPlatebody");
            this.IronPlatelegs = ItemDictionary.Instance.Get("IronPlatelegs");
            this.IronKiteshield = ItemDictionary.Instance.Get("IronKiteshield");
            this.IronLgSetItem = ItemDictionary.Instance.Get("IronSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.IronFullHelm, 1),
                new InventoryItem(this.IronPlatebody, 1),
                new InventoryItem(this.IronPlatelegs, 1),
                new InventoryItem(this.IronKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.IronLgSetItem;
        }
    }
}
