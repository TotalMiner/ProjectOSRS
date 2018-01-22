using StudioForge.TotalMiner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectOSRS.Sets
{
    class HalloweenMaskSet : Set
    {
        private Item RedHalloweenMask;
        private Item GreenHalloweenMask;
        private Item BlueHalloweenMask;
        private Item HalloweenMaskSetItem;

        public HalloweenMaskSet()
        {
            this.RedHalloweenMask = ItemDictionary.Instance.Get("RedHalloweenMask");
            this.GreenHalloweenMask = ItemDictionary.Instance.Get("GreenHalloweenMask");
            this.BlueHalloweenMask = ItemDictionary.Instance.Get("BlueHalloweenMask");
            this.HalloweenMaskSetItem = ItemDictionary.Instance.Get("HalloweenMaskSet");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[] {
                new InventoryItem(this.RedHalloweenMask, 1),
                new InventoryItem(this.GreenHalloweenMask, 1),
                new InventoryItem(this.BlueHalloweenMask, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.HalloweenMaskSetItem;
        }
    }
}
