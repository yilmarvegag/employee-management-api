using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Filters;
using EmployeeManagement.API.Middlewares;
using EmployeeManagement.Application;
using EmployeeManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Composition Root
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddJwtAuthentication();

// Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilterAttribute>();
    options.Conventions.Add(
        new RouteTokenTransformerConvention(
            new LowercaseControllerTransformer()));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerWithJwt();

// API Versioning
builder.Services.ConfigureApiVersioning();

// Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Apply database migrations and seed data
if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync();
}

app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
