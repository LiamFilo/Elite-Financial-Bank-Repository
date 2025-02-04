using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using BankingEnumeration;

namespace BankingCommunication.Convertor_classes
{
    public class IPacketConverter : JsonConverter<IPacket>
    {
        public override IPacket Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // נשתמש ב-JsonDocument כדי לקרוא את כל האובייקט
            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = document.RootElement;

                // נבדוק אם קיים שדה "Type"
                if (!root.TryGetProperty("Type", out JsonElement typeProperty))
                {
                    throw new JsonException("Missing property 'Type' in JSON for IPacket.");
                }

                // סגנון 1: אם ה-Type מגיע כ-int (כמו הערכים של ה־enum)
                // אפשר לעשות:
                // BCPPacketType packetType = (BCPPacketType)typeProperty.GetInt32();

                // סגנון 2: אם ה-Type מגיע כ-string (למשל "Request" / "Response")
                // אפשר לעשות:
                BCPPacketType packetType;
                if (typeProperty.ValueKind == JsonValueKind.String)
                {
                    // ניסיון להמיר מחרוזת ל-enum
                    string typeString = typeProperty.GetString();
                    if (!Enum.TryParse(typeString, out packetType))
                    {
                        throw new JsonException($"Unknown PacketType: {typeString}");
                    }
                }
                else
                {
                    // ברירת מחדל – נניח שהערך מגיע כ-int
                    packetType = (BCPPacketType)typeProperty.GetInt32();
                }

                // לפי הערך של packetType נחליט איזה סוג Packet ליצור
                Type targetType = packetType switch
                {
                    BCPPacketType.Request => typeof(BCPPacketRequest),
                    BCPPacketType.Response => typeof(BCPPacketResponse),
                    _ => throw new JsonException($"Unknown PacketType: {packetType}")
                };

                // נעשה דסיריאליזציה לסוג הקונקרטי
                string json = root.GetRawText();
                IPacket result = (IPacket)JsonSerializer.Deserialize(json, targetType, options);
                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, IPacket value, JsonSerializerOptions options)
        {
            // בעת כתיבה, נשתמש בסוג הקונקרטי בפועל
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
