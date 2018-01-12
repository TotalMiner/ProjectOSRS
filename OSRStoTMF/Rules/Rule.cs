using OSRStoTMF.OSRS;
using OSRStoTMF.TotalMiner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF.Rules
{
    public abstract class Rule
    {
        public abstract bool ShouldTransform(string id, OSRSItem osrsItem);

        public abstract ModItemDataXML TransformItemData(string id, OSRSItem osrsItem, ModItemDataXML modItemData);
        public abstract ModItemTypeDataXML TransformItemTypeData(string id, OSRSItem osrsItem, ModItemTypeDataXML modItemTypeData);
    }
}
