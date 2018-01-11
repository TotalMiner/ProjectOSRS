using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF
{
    class ItemSchema
    {
        [JsonProperty("item")]
        public Dictionary<string, Item> Items { get; set; }
    }
}
