using RainfallAPI.Models;
using RainfallAPI.Models.Response;

namespace RainfallAPI.Utilities.Helpers
{
    public class InternalServerErrorExceptionHelper : Exception
    {
        public ErrorResponse ErrorDetails { get; }

        public InternalServerErrorExceptionHelper(string message, string details = "") : base(message)
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
