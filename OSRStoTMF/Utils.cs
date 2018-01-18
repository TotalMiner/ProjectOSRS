using OSRStoTMF.OSRS;
using OSRStoTMF.TotalMiner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF
{
    public static class Utils
    {
        public static EquipIndex RStoTMSlot(OSRSItemSlot slot)
        {
            switch(slot)
            {
                case OSRSItemSlot.Hands:
                    return EquipIndex.LeftSide;
                case OSRSItemSlot.Ring:
                    return EquipIndex.RightSide;
                case OSRSItemSlot.Head:
                    return EquipIndex.Head;
                case OSRSItemSlot.Body:
                    return EquipIndex.Body;
                case OSRSItemSlot.Legs:
                    return EquipIndex.Legs;
                case OSRSItemSlot.Feet:
                    return EquipIndex.Feet;
                case OSRSItemSlot.Cape:
                case OSRSItemSlot.Neck:
                    return EquipIndex.Neck;
                case OSRSItemSlot.TwoHanded:
                case OSRSItemSlot.Weapon:
                    return EquipIndex.RightHand;
                case OSRSItemSlot.Shield:
                case OSRSItemSlot.Ammo:
                default:
                    return EquipIndex.LeftHand;
            }
        }
    }
}
