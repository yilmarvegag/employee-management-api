using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Extensions
{
    public static class ApiVersioningBuilderExtensions
    {
        /// <summary>
        /// Configures API versioning and related behavior for the application's service collection.
        /// </summary>
        /// <remarks>This method sets up default API versioning, enables reporting of supported API
        /// versions, and configures version reading from URL segments, headers, and media types. It also customizes API
        /// explorer grouping and suppresses certain model state and client error behaviors. Call this method during
        /// application startup to ensure consistent API versioning across controllers.</remarks>
        /// <param name="services">The service collection to which API versioning and related options will be added. Must not be null.</param>
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("v")
                );
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
            });
        }
    }
}
