using RainfallAPI.Business.Service.Contract;
using RainfallAPI.Models;
using RainfallAPI.Models.Response;
using RainfallAPI.Models.Response.JSONResponses;
using RainfallAPI.Utilities.Helpers;
using System.Net;
using System.Text.Json;

namespace RainfallAPI.Business.Service.Implementation
{
    public class RainfallService: IRainfallService
    {
        private readonly HttpClient _httpClient;

        public RainfallService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RainfallReadingResponse> GetRainfallReadingsAsync(string stationId, int count = 10)
        {
            var response = await _httpClient.GetAsync($"/flood-monitoring/id/stations/{stationId}/readings?_sorted&_limit={count}");
            return await HandleResponseAsync<RainfallReadingResponse>(response);
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            if (response.IsSuccessStatusCode)
            {
                var successResponse = await ParseSuccessResponseAsync(response);
                return successResponse as T;
            }

            var errorResponse = await ParseErrorResponseAsync(response);
            return errorResponse as T;
        }

        private async Task<RainfallReadingResponse> ParseSuccessResponseAsync(HttpResponseMessage response)
        {
            List<RainfallReadingItem> readingItems = new List<RainfallReadingItem>();

            var jsonString = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonSerializer.Deserialize<RainfallReadingJSONResponse>(jsonString);

            if (parsedResponse != null)
            {
                readingItems = (from r in parsedResponse.Items
                                select new RainfallReadingItem()
                                {
                                    DateMeasured = r.DateTime.ToShortDateString(),
                                    AmountMeasured = r.Value
                                }).ToList();
            }

            return new RainfallReadingResponse() { Readings = readingItems };
        }

        private async Task<ErrorResponse> ParseErrorResponseAsync(HttpResponseMessage response)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    var notFoundResponse = ExtractErrorMessageFromHtml(jsonString);
                    throw new NotFoundExceptionHelper("No readings found for the specified stationId", notFoundResponse);

                case HttpStatusCode.BadRequest:
                    throw new BadRequestExceptionHelper("Invalid request", jsonString);

                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorExceptionHelper("Internal server error", jsonString);

                default:
                    // Handle other cases here
                    return new ErrorResponse()
                    {
                        Message = "Unhandled status code",
                        Details = null
                    };
            }
        }

        private string ExtractErrorMessageFromHtml(string htmlContent)
        {
            // Implement logic to extract error message from HTML content
            var startTag = "<h1>";
            var endTag = "</h1>";
            var startIndex = htmlContent.IndexOf(startTag) + startTag.Length;
            var endIndex = htmlContent.IndexOf(endTag, startIndex);

            return htmlContent.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}