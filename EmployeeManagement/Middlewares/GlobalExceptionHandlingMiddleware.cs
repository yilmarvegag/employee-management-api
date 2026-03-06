using EmployeeManagement.Application.Exceptions;
using EmployeeManagement.Domain.Common.Responses;
using EmployeeManagement.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace EmployeeManagement.API.Middlewares
{
    /// <summary>
    /// Provides middleware for handling exceptions globally in an ASP.NET Core application. Captures unhandled
    /// exceptions during request processing and returns standardized error responses.
    /// </summary>
    /// <remarks>This middleware should be registered early in the request pipeline to ensure that exceptions
    /// are caught and handled consistently. It logs unhandled exceptions and formats error responses as JSON, including
    /// relevant status codes and trace identifiers. Use this middleware to centralize error handling and improve API
    /// reliability.</remarks>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context/*, RequestDelegate next*/)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int httpStatusCode = (int)HttpStatusCode.BadRequest;
            string httpStatusMessage = HttpStatusCode.BadRequest.ToString();
            context.Response.StatusCode = httpStatusCode;

            Response<Object> responseApi = new()
            {
                Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/400",
                Instance = context.Request.Path
            };

            string result;

            switch (exception)
            {
                case InvalidModelException ex:
                    context.Response.StatusCode = httpStatusCode;
                    responseApi.Title = httpStatusMessage;
                    responseApi.Status = httpStatusCode;
                    responseApi.Message = ex.Message;
                    responseApi.Errors = ex.Errors;
                    break;
                case EmployeeManagementDomainException ex:
                    context.Response.StatusCode = httpStatusCode;
                    responseApi.Title = httpStatusMessage;
                    responseApi.Status = httpStatusCode;
                    responseApi.Message = ex.Message;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseApi.Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500";
                    responseApi.Title = HttpStatusCode.InternalServerError.ToString();
                    responseApi.Status = (int)HttpStatusCode.InternalServerError;
                    responseApi.Message = exception.Message;
                    break;
            }

            responseApi.TraceId = context.TraceIdentifier;
            result = JsonSerializer.Serialize(responseApi);
            await context.Response.WriteAsync(result);
        }
    }
}
