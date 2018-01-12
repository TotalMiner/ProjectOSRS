using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF.OSRS
{
    class OSRSItemSchema
    {
        [JsonProperty("item")]
        public Dictionary<string, OSRSItem> Items { get; set; }
    }
}
