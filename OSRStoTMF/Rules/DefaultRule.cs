using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSRStoTMF.OSRS;
using OSRStoTMF.TotalMiner;

namespace OSRStoTMF.Rules
{
    public class DefaultRule : Rule
    {
        public override bool ShouldTransform(string id, OSRSItem osrsItem)
        {
            return true;
        }

        public override ModItemDataXML TransformItemData(string id, OSRSItem osrsItem, ModItemDataXML modItemData)
        {
            modItemData.ItemID = id;
            modItemData.Name = osrsItem.Name;
            modItemData.Desc = osrsItem.Description;
            modItemData.IsValid = true;
            modItemData.IsEnabled = true;
            modItemData.LockedDD = true;
            modItemData.LockedCR = false;
            modItemData.LockedSU = true;
            modItemData.MinCSPrice = -1;
            if (osrsItem.CanStack)
            {
                modItemData.StackSize = int.MaxValue;
            }
            else
            {
                modItemData.StackSize = 1;
            }
            modItemData.Durability = 0;
            modItemData.StrikeDamage = 0;
            modItemData.StrikeReach = 0;
            modItemData.HealPower = 0;
            modItemData.BurnTime = 0;
            modItemData.SmeltTime = 0;
            modItemData.ParticleLight = 0;
            modItemData.CanDropIfLocked = false;
            modItemData.DropChance = 0;
            modItemData.Plural = PluralType.None;

            return modItemData;
        }

        public override ModItemTypeDataXML TransformItemTypeData(string id, OSRSItem osrsItem, ModItemTypeDataXML modItemTypeData)
        {
            modItemTypeData.ItemID = id;
            modItemTypeData.Use = ItemUse.Item;
            modItemTypeData.Type = ItemType.Item;
            modItemTypeData.SubType = ItemSubType.None;
            modItemTypeData.Class = ItemTypeClass.None;
            modItemTypeData.Inv = ItemInvType.Other;
            modItemTypeData.Combat = CombatItem.None;
            modItemTypeData.Model = ItemModelType.MediumItem;
            modItemTypeData.Swing = ItemSwingType.Item;
            modItemTypeData.Equip = Utils.RStoTMSlot(osrsItem.Slot);

            return modItemTypeData;
        }
    }
}
