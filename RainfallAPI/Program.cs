using Microsoft.OpenApi.Models;
using RainfallAPI.Business.Service.Contract;
using RainfallAPI.Business.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.

var httpClient = new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["BaseApiUrl"])
};

builder.Services.AddSingleton(httpClient);

// Add services to the container.
builder.Services.AddScoped<IRainfallService, RainfallService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Rainfall API",
            Version = "1.0",
            Contact = new OpenApiContact
            {
                Name = "Sorted",
                Url = new Uri("https://www.sorted.com")
            },
            Description = "An API which provides rainfall reading data"
        });

        c.EnableAnnotations();
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rainfall API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
