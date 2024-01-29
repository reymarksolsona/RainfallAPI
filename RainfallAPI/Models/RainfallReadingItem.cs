using System.Text.Json.Serialization;

namespace RainfallAPI.Models
{
    public class RainfallReadingItem
    {
        public string DateMeasured { get; set; }
        public decimal AmountMeasured { get; set; }
    }
}
