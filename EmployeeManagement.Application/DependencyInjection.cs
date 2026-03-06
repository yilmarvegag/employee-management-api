using EmployeeManagement.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Application
{
    /// <summary>
    /// Provides extension methods for registering application-level services and dependencies with the dependency
    /// injection container.
    /// Ref: https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/
    /// </summary>
    /// <remarks>This class is intended to be used during application startup to configure services such as
    /// MediatR, AutoMapper, and validation pipelines. All methods are static and designed to be called on an instance
    /// of <see cref="IServiceCollection"/>.</remarks>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            var assembly = AssemblyReference.Assembly;

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(assembly));

            //services.AddAutoMapper(typeof(AssemblyReference));
            services.AddAutoMapper(cfg => { }, AssemblyReference.Assembly);


            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehavior<,>));

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingPipelineBehavior<,>));

            // Register all validators from the assembly, including internal types
            services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

            return services;
        }
    }
}
