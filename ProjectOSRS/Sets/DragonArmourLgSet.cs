using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge.TotalMiner;

namespace ProjectOSRS.Sets
{
    class DragonArmourLgSet : Set
    {
        private Item DragonFullHelm;
        private Item DragonPlatebody;
        private Item DragonPlatelegs;
        private Item DragonKiteshield;
        private Item DragonLgSetItem;

        public DragonArmourLgSet()
        {
            this.DragonFullHelm = ItemDictionary.Instance.Get("DragonFullHelm");
            this.DragonPlatebody = ItemDictionary.Instance.Get("DragonPlatebody");
            this.DragonPlatelegs = ItemDictionary.Instance.Get("DragonPlatelegs");
            this.DragonKiteshield = ItemDictionary.Instance.Get("DragonKiteshield");
            this.DragonLgSetItem = ItemDictionary.Instance.Get("DragonArmourSetLg");
        }

        public override IList<InventoryItem> GetItems()
        {
            return new[]
            {
                new InventoryItem(this.DragonFullHelm, 1),
                new InventoryItem(this.DragonPlatebody, 1),
                new InventoryItem(this.DragonPlatelegs, 1),
                new InventoryItem(this.DragonKiteshield, 1)
            };
        }

        public override Item GetSetItem()
        {
            return this.DragonLgSetItem;
        }
    }
}
