using Microsoft.AspNetCore.Mvc;
using Moq;
using RainfallAPI.Business.Service.Contract;
using RainfallAPI.Controllers;
using RainfallAPI.Models.Response;
using RainfallAPI.Utilities.Helpers;

namespace RainfallAPI.Tests.Tests
{
    public class RainfallControllerTest
    {
        [Fact]
        public async Task GetRainfallReadings_ReturnsOkObjectResult_WhenServiceReturnsData()
        {
            // Arrange
            var mockService = new Mock<IRainfallService>();
            mockService.Setup(service => service.GetRainfallReadingsAsync(It.IsAny<string>(), It.IsAny<int>()))
                       .ReturnsAsync(new RainfallReadingResponse());

            var controller = new RainfallController(mockService.Object);

            // Act
            var result = await controller.GetRainfallReadings("stationId", 10) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<RainfallReadingResponse>(result.Value);
        }

        [Fact]
        public async Task GetRainfallReadings_ReturnsStatusCode400_WhenServiceThrowsBadRequestException()
        {
            // Arrange
            var mockService = new Mock<IRainfallService>();
            mockService.Setup(service => service.GetRainfallReadingsAsync(It.IsAny<string>(), It.IsAny<int>()))
                       .ThrowsAsync(new BadRequestExceptionHelper("Test exception"));

            var controller = new RainfallController(mockService.Object);

            // Act
            var result = await controller.GetRainfallReadings("stationId", 10) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.IsType<ErrorResponse>(result.Value);
        }

        [Fact]
        public async Task GetRainfallReadings_ReturnsStatusCode404_WhenServiceThrowsNotFoundException()
        {
            // Arrange
            var mockService = new Mock<IRainfallService>();
            mockService.Setup(service => service.GetRainfallReadingsAsync(It.IsAny<string>(), It.IsAny<int>()))
                       .ThrowsAsync(new NotFoundExceptionHelper("Test exception"));

            var controller = new RainfallController(mockService.Object);

            // Act
            var result = await controller.GetRainfallReadings("stationId", 10) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.IsType<ErrorResponse>(result.Value);
        }

        [Fact]
        public async Task GetRainfallReadings_ReturnsStatusCode500_WhenServiceThrowsInternalServerErrorException()
        {
            // Arrange
            var mockService = new Mock<IRainfallService>();
            mockService.Setup(service => service.GetRainfallReadingsAsync(It.IsAny<string>(), It.IsAny<int>()))
                       .ThrowsAsync(new InternalServerErrorExceptionHelper("Test exception"));

            var controller = new RainfallController(mockService.Object);

            // Act
            var result = await controller.GetRainfallReadings("stationId", 10) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.IsType<ErrorResponse>(result.Value);
        }
    }
}