using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF.TotalMiner
{
    public struct ModItemTypeDataXML
    {
        public string ItemID;
        public ItemUse Use;
        public ItemType Type;
        public ItemSubType SubType;
        public ItemTypeClass Class;
        public ItemInvType Inv;
        public CombatItem Combat;
        public ItemModelType Model;
        public ItemSwingType Swing;
        public EquipIndex Equip;
    }
}
