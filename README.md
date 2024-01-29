
Rainfall API
This project implements a simple Rainfall API using ASP.NET Core (.net 6), which provides a service to retrieve rainfall readings by station ID. The API handles different scenarios such as successful data retrieval, invalid requests, not found scenarios, and internal server errors.

RainfallController
The RainfallController is responsible for handling HTTP requests related to rainfall readings. It utilizes the IRainfallService to interact with the external API that provides rainfall data.

Endpoints
Get Rainfall Readings
Endpoint: /api/rainfall/readings/{stationId}
Method: GET
Parameters:
stationId (path): The ID of the reading station.
count (query, optional): The number of readings to return (default is 10).
Responses:
200 OK: A list of rainfall readings successfully retrieved.
400 Bad Request: Invalid request.
404 Not Found: No readings found for the specified stationId.
500 Internal Server Error: An internal server error occurred.
RainfallService
The RainfallService is responsible for interacting with the external API that provides rainfall data. It uses an instance of HttpClient to make requests and handles the responses accordingly.

Unit Tests
The unit tests, located in the RainfallControllerTest class, use xUnit and Moq to test the behavior of the GetRainfallReadings method in the RainfallController. These tests cover scenarios such as successful data retrieval, handling of bad requests, not found scenarios, and internal server errors.

Test Cases
GetRainfallReadings_ReturnsOkObjectResult_WhenServiceReturnsData:

Arrange: Mock the service to return rainfall data.
Act: Call the controller method.
Assert: Check that the response is an OK result with the expected status code and type.
GetRainfallReadings_ReturnsStatusCode400_WhenServiceThrowsBadRequestException:

Arrange: Mock the service to throw a BadRequestException.
Act: Call the controller method.
Assert: Check that the response is a Bad Request result with the expected status code and type.
GetRainfallReadings_ReturnsStatusCode404_WhenServiceThrowsNotFoundException:

Arrange: Mock the service to throw a NotFoundException.
Act: Call the controller method.
Assert: Check that the response is a Not Found result with the expected status code and type.
GetRainfallReadings_ReturnsStatusCode500_WhenServiceThrowsInternalServerErrorException:

Arrange: Mock the service to throw an InternalServerErrorException.
Act: Call the controller method.
Assert: Check that the response is an Internal Server Error result with the expected status code and type.
How to Run
To run the project and execute the unit tests:

Ensure you have the necessary dependencies installed (.Net 6).
Build the solution using your preferred IDE or command line.
Run the API project.
Run the unit tests to verify the functionality of the RainfallController.
