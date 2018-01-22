using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class RuneArmourLgSet : Set
    {
        private Item RuneFullHelm;
        private Item RunePlatebody;
        private Item RunePlatelegs;
        private Item RuneKiteshield;
        private Item RuneLgSetItem;

        public RuneArmourLgSet()
        {
            this.RuneFullHelm = ItemDictionary.Instance.Get("RuneFullHelm");
            this.RunePlatebody = ItemDictionary.Instance.Get("RunePlatebody");
            this.RunePlatelegs = ItemDictionary.Instance.Get("RunePlatelegs");
            this.RuneKiteshield = ItemDictionary.Instance.Get("RuneKiteshield");
            this.RuneLgSetItem = ItemDictionary.Instance.Get("RuneArmourSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.RuneFullHelm, 1),
                new InventoryItem(this.RunePlatebody, 1),
                new InventoryItem(this.RunePlatelegs, 1),
                new InventoryItem(this.RuneKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.RuneLgSetItem;
        }
    }
}
