using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eventshuffle.Utils
{
    public class InputDateTimeConverter : JsonConverter<List<DateTime>>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override List<DateTime> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dates = new List<DateTime>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                var dateString = reader.GetString();
                dates.Add(DateTime.ParseExact(dateString, DateFormat, null));
            }
            return dates;
        }

        public override void Write(Utf8JsonWriter writer, List<DateTime> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var date in value)
            {
                writer.WriteStringValue(date.ToString(DateFormat));
            }
            writer.WriteEndArray();
        }
    }
}
