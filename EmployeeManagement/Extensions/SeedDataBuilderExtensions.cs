using EmployeeManagement.Persistence.Context;
using EmployeeManagement.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Extensions
{
    /// <summary>
    /// Initializes the database from scratch and create the data seed.
    /// </summary>
    public static class SeedDataBuilderExtensions
    {
        public static async Task MigrateDatabase(this WebApplication webApp)
        {
            // Create a service scope
            using var scope = webApp.Services.CreateScope(); 

            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<EmployeeManagementContext>();
                // Apply pending migrations asynchronously
                await context.Database.MigrateAsync();
                // Check if the database can be connected
                if (context.Database.CanConnect())
                {
                    // Apply pending migrations
                    context.Database.Migrate(); 
                    Console.WriteLine("Migrations applied correctly");
                    //
                    await UserSeed.SeedAsync(context);
                    await DepartmentSeed.SeedAsync(context);
                    await EmployeeSeeder.SeedAsync(context);
                    await ProjectSeed.SeedAsync(context);
                    await EmployeeProjectSeed.SeedAsync(context);
                    //
                    Console.WriteLine("Seeds applied correctly");
                }
                else
                {
                    Console.WriteLine("Unable to connect to the database");
                    // If the database cannot be connected, you might want to create it or handle this case as needed
                    //context.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Error during database migration");
                //Rerun the exception so that it fails visibly.
                throw;
            }
        }
    }
}
