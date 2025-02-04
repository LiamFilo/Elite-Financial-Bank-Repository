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
    public class ICommandConverter : JsonConverter<ICommand>
    {
        public override ICommand Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // משתמשים ב-JsonDocument כדי לקרוא את ה-JSON השלם
            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = document.RootElement;

                // מחפשים את השדה "CommandType"
                if (!root.TryGetProperty(nameof(ICommand.CommandType), out JsonElement commandTypeProperty))
                {
                    throw new JsonException("Missing property 'CommandType' in JSON for ICommand.");
                }

                // נמיר את ערך ה-CommandType ל-enum ClientCommandType
                ClientCommandType commandType;

                // אם ב-JSON ה-CommandType מגיע כמספר:
                if (commandTypeProperty.ValueKind == JsonValueKind.Number)
                {
                    commandType = (ClientCommandType)commandTypeProperty.GetInt32();
                }
                // ואם הוא מגיע כמחרוזת:
                else
                {
                    string commandTypeString = commandTypeProperty.GetString();
                    if (!Enum.TryParse(commandTypeString, out commandType))
                    {
                        throw new JsonException($"Unknown CommandType: {commandTypeString}");
                    }
                }

                // לפי ערך ה-CommandType נחליט על המחלקה הקונקרטית
                Type targetType = commandType switch
                {
                    ClientCommandType.Log_In => typeof(LogInCommand),
                    ClientCommandType.Transfer=> typeof(TransferCommand),
                    ClientCommandType.Withdraw => typeof(WithdrawCommand),
                    ClientCommandType.Deposit => typeof(DepositCommand),
                    ClientCommandType.Register => typeof(RegisterCommand),
                    // הוסיפו כאן case-ים נוספים לפי שאר הערכים ב-ClientCommandType
                    _ => typeof(NonArgumentClientCommnd)
                };

                // נעשה Deserialize למחלקה שהתאמנו
                string json = root.GetRawText();
                ICommand result = (ICommand)JsonSerializer.Deserialize(json, targetType, options);
                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, ICommand value, JsonSerializerOptions options)
        {
            // בעת כתיבה, פשוט נשתמש בסוג הקונקרטי
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
}
