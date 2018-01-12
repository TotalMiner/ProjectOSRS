using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF.OSRS
{
    public class OSRSItemStats
    {
        [JsonProperty("attack")]
        public Stats Attack { get; set; }
        [JsonProperty("bonus")]
        public Stats Bonus { get; set; }
        [JsonProperty("defence")]
        public Stats Defence { get; set; }
    }

    public struct Stats
    {
        [JsonProperty("crush")]
        public int Crush { get; set; }
        [JsonProperty("magic")]
        public int Magic { get; set; }
        [JsonProperty("range")]
        public int Range { get; set; }
        [JsonProperty("slash")]
        public int Slash { get; set; }
        [JsonProperty("stab")]
        public int Stab { get; set; }
    }
}
