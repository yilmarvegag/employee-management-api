using EmployeeManagement.API.OptionsSetup.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.API.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring JWT-based authentication and authorization in an ASP.NET Core
    /// application.
    /// </summary>
    /// <remarks>This class contains static methods that extend the IServiceCollection to add JWT
    /// authentication and set up authorization policies. Use these extensions during application startup to enable
    /// secure access control based on JSON Web Tokens (JWT).</remarks>
    public static class AuthenticationExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.AddAuthentication(options =>
            {
                //Sets the JWT authentication scheme as the default scheme.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //Sets the JWT authentication scheme as the default challenge scheme.
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                //Stores JWT access token in authentication object.
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }
    }
}
