using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Infrastructure.Authentication;
using EmployeeManagement.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddPersistence(configuration);

            var jwtSection = configuration.GetSection("Jwt");

            services.Configure<JwtOptions>(jwtSection);

            services.AddScoped<ITokenService, JwtTokenService>();

            services.AddScoped<PasswordHasher>();

            return services;
        }
    }
}
