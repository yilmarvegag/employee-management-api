using Microsoft.OpenApi;

namespace EmployeeManagement.API.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring Swagger documentation with JWT Bearer authentication in an ASP.NET
    /// Core application.
    /// </summary>
    /// <remarks>This class enables the integration of JWT Bearer security schemes into Swagger UI, allowing
    /// API endpoints to be documented and tested with JWT authentication. Use these extensions to add standardized
    /// security definitions and requirements to your Swagger setup. This is intended for use with ASP.NET Core
    /// dependency injection and Swagger generation.</remarks>
    public static class SwaggerJwtExtensions
    {
        public static void AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Employee Management",
                    Version = "v1",
                    Description = "Employee Management API",
                    Contact = new OpenApiContact
                    {
                        Name = "Employee Management - EMI",
                        Email = "soporte@employeemanagement.com"
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter the JWT token without the ‘Bearer’ prefix, for example: **eyJhbGciOiJIUzI1NiIs...**",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(document => new() { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });

            });
        }
    }
}
