using System.Text.Json.Serialization;

namespace RainfallAPI.Models.Response
{
    public class RainfallReadingResponse
    {
        public List<RainfallReadingItem> Readings { get; set; }
    }
}
