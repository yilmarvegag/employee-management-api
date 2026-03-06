using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace EmployeeManagement.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<EmployeeManagementContext>(options =>
                options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sql =>
                {
                    sql.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));

            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();

            // Register all repositories in the assembly using Scrutor
            services.Scan(
               selector => selector
               .FromAssemblies(AssemblyReference.Assembly)
               .AddClasses(false)
               .UsingRegistrationStrategy(RegistrationStrategy.Skip)
               .AsImplementedInterfaces()
               .WithScopedLifetime());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
