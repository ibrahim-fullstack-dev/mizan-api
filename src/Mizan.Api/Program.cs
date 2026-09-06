using Mizan.Api.Common.Exceptions;
using Mizan.Application;
using Mizan.Infrastructure;

/// <summary>
/// 1- Prepare the application.
/// 2- Register services that application will need.
/// 3- build the application.
/// 4- Select how the HTTP request will pass.
/// 5- Start receiving HTTP requests.
/// </summary>

// Composition Root
var builder = WebApplication.CreateBuilder(args);

// Configure Services

// Register Application services.
builder.Services.AddApplication();

// Register Infrastructure services.
builder.Services.AddInfrastructure(builder.Configuration);

// Register global exception handling.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Register API services.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build Application
var app = builder.Build();

// HTTP Request Pipeline

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Map controller endpoints.
app.MapControllers();

app.Run();