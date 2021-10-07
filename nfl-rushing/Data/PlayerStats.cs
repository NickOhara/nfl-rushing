using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace nfl_rushing
{
    public class PlayerStats
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Player { get; set; }
        public string Team { get; set; }
        public string Pos { get; set; }
        public double Att { get; set; }
        [JsonPropertyName("Att/G")]
        public double AttPerG { get; set; }
        public double Yds { get; set; }
        public double Avg { get; set; }

        [JsonPropertyName("Yds/G")]
        public double YdsPerG { get; set; }
        public int TD { get; set; }
        public string Lng { get; set; }

        [JsonPropertyName("1st")]
        public double First { get; set; }
        [JsonPropertyName("1st%")]
        public double FirstPercentage { get; set; }

        [JsonPropertyName("20+")]
        public int TwentyPlus { get;set; }
        [JsonPropertyName("40+")]
        public int FortyPlus { get; set; }
        public int FUM { get; set; }
    }

    public class StringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            if (reader.TokenType == JsonTokenType.Number)
            {
                var stringValue = reader.GetInt32();
                return stringValue.ToString();
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }

    public class NumberConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                return Double.Parse(stringValue.Replace(",",""));
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDouble();
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
