using Microsoft.AspNetCore.Mvc;
using RainfallAPI.Business.Service.Contract;
using RainfallAPI.Models.Response;
using RainfallAPI.Utilities.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RainfallAPI.Controllers
{
    [ApiController]
    [Route("api/rainfall")]
    public class RainfallController : ControllerBase
    {
        private readonly IRainfallService _rainfallService;

        public RainfallController(IRainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        /// <summary>
        /// Get rainfall readings by station Id
        /// </summary>
        /// <param name="stationId">The id of the reading station</param>
        /// <param name="count">The number of readings to return (default is 10)</param>
        /// <returns>A list of rainfall readings successfully retrieved</returns>
        [HttpGet("readings/{stationId}")]
        [SwaggerResponse(200, "A list of rainfall readings successfully retrieved", typeof(RainfallReadingResponse))]
        [SwaggerResponse(400, "Invalid request", typeof(ErrorResponse))]
        [SwaggerResponse(404, "No readings found for the specified stationId", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public async Task<IActionResult> GetRainfallReadings(
            [FromRoute] string stationId,
            [FromQuery][Range(1, 100, ErrorMessage = "Count must be between 1 and 100")] int count = 10)
        {
            try
            {
                var readings = await _rainfallService.GetRainfallReadingsAsync(stationId, count);
                return Ok(readings);
            }
            catch (BadRequestExceptionHelper ex)
            {
                var badRequestResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    Details = ex.ErrorDetails.Details
                };

                return StatusCode(400, badRequestResponse);
            }
            catch (NotFoundExceptionHelper ex)
            {
                var badRequestResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    Details = ex.ErrorDetails.Details
                };

                return StatusCode(404, badRequestResponse);
            }
            catch (InternalServerErrorExceptionHelper ex)
            {
                var internalServerErrorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    Details = ex.ErrorDetails.Details // Populate with specific details if needed
                };

                return StatusCode(500, internalServerErrorResponse);
            }
        }
    }
}
