using RainfallAPI.Models.Response;

namespace RainfallAPI.Business.Service.Contract
{
    public interface IRainfallService
    {
        Task<RainfallReadingResponse> GetRainfallReadingsAsync(string stationId, int count = 10);
    }
}
