using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF.OSRS
{
    public class OSRSItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("noted")]
        public int NotedId { get; set; }

        [JsonProperty("high_alch")]
        public int HighAlch { get; set; }

        [JsonProperty("low_alch")]
        public int LowAlch { get; set; }

        [JsonProperty("attack_speed")]
        public float AttackSpeed { get; set; }

        [JsonProperty("is_in_exchange")]
        public bool InExchange { get; set; }
        
        [JsonProperty("stackable")]
        public bool CanStack { get; set; }

        [JsonProperty("equipable")]
        public bool Equippable { get; set; }

        [JsonProperty("tradeable")]
        public bool Tradeable { get; set; }

        [JsonProperty("quest_item")]
        public bool QuestItem { get; set; }

        [JsonProperty("members")]
        public bool Members { get; set; }

        [JsonProperty("has_noted")]
        public bool HasNoted { get; set; }

        [JsonProperty("slot")]
        [JsonConverter(typeof(OSRSItemSlotConverter))]
        public OSRSItemSlot Slot { get; set; }

        [JsonProperty("stats")]
        public OSRSItemStats Stats { get; set; }
    }
}
