using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSRStoTMF.OSRS
{
    public class OSRSItemSlotConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            OSRSItemSlot itemSlot = (OSRSItemSlot)value;

            switch (itemSlot)
            {
                case OSRSItemSlot.TwoHanded:
                    writer.WriteValue("2h");
                    break;
                default:
                    writer.WriteValue(itemSlot.ToString().ToLower());
                    break;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;

            switch (enumString)
            {
                case "2h":
                    return OSRSItemSlot.TwoHanded;
                default:
                    if (Enum.TryParse(enumString, true, out OSRSItemSlot slot)) {
                        return slot;
                    } else
                    {
                        return OSRSItemSlot.None;
                    }
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
