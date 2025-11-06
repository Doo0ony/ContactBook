
using ContactBook.Api.Middleware;
using ContactBook.Application.Extensions;
using ContactBook.Infrastructure.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddOpenApi();

// Add Infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application layer
builder.Services.AddApplicationServices();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Add AutoMapper
builder.Services.AddMappingProfiles(builder.Configuration);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5071") // blazor client origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

//add custom middleware
app.UseMiddleware<RequestLoggingMiddleware>();

//use CORS policy
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
