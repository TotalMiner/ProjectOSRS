using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSRStoTMF.OSRS;
using OSRStoTMF.TotalMiner;

namespace OSRStoTMF.Rules
{
    class PartyhatRule : Rule
    {
        public override bool ShouldTransform(string id, OSRSItem osrsItem)
        {
            switch(id)
            {
                case "RedPartyhat":
                case "BluePartyhat":
                case "YellowPartyhat":
                case "PurplePartyhat":
                case "WhitePartyhat":
                case "GreenPartyhat":
                    return true;
                default:
                    return false;
            }
        }

        public override ModItemDataXML TransformItemData(string id, OSRSItem osrsItem, ModItemDataXML modItemData)
        {
            return modItemData;
        }

        public override ModItemTypeDataXML TransformItemTypeData(string id, OSRSItem osrsItem, ModItemTypeDataXML modItemTypeData)
        {
            modItemTypeData.Inv = ItemInvType.Jewelry;
            return modItemTypeData;
        }
    }
}
