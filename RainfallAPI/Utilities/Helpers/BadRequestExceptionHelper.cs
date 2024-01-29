using RainfallAPI.Models;
using RainfallAPI.Models.Response;

namespace RainfallAPI.Utilities.Helpers
{
    public class BadRequestExceptionHelper : Exception
    {
        public ErrorResponse ErrorDetails { get; }

        public BadRequestExceptionHelper(string message, string details = "") : base(message)
        {
            ErrorDetails = new ErrorResponse
            {
                Message = message,
                Details = new List<ErrorDetail>
                {
                    new ErrorDetail
                    {
                        Message = details,
                        PropertyName = null
                    }
                }
            };
        }
    }
}
