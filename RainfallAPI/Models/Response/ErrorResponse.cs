using System.Net;

namespace RainfallAPI.Models.Response
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<ErrorDetail> Details { get; set; }
    }
}
