using System.Text.Json.Serialization;

namespace RainfallAPI.Models.Response.JSONResponses
{
    public class RainfallReadingJSONResponse
    {
        [JsonPropertyName("items")]
        public List<RainfallReadingItemJSONResponse> Items { get; set; }
    }
}
