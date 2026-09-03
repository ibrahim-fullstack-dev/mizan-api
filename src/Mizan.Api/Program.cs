// src/Mizan.Api/Program.cs

using Mizan.Api.Common.Exceptions;
using Mizan.Application;
using Mizan.Infrastructure;

// Set up ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// Register Application Dependency Injection
builder.Services.AddApplication();

// Register Infrastructure Dependency Injection
builder.Services.AddInfrastructure(builder.Configuration);

// handle global exceptions
builder.Services.AddExceptionHandler<
    GlobalExceptionHandler>();

// handle problem details
builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();