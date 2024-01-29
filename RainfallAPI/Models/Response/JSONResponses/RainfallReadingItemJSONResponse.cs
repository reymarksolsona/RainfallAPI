using System.Text.Json.Serialization;

namespace RainfallAPI.Models.Response.JSONResponses
{
    public class RainfallReadingItemJSONResponse
    {
        [JsonPropertyName("@id")]
        public string Id { get; set; }

        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("measure")]
        public string Measure { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
