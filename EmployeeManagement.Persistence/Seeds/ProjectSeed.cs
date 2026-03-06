using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Context;

namespace EmployeeManagement.Persistence.Seeds
{
    public static class ProjectSeed
    {
        public static async Task SeedAsync(EmployeeManagementContext context)
        {
            if (context.Projects.Any())
                return;

            var projects = new List<Project>
            {
                new("ERP System"),
                new("Mobile Banking"),
                new("HR Platform"),
                new("E-commerce Website"),
                new("Data Analytics Dashboard"),
            };

            await context.Projects.AddRangeAsync(projects);

            await context.SaveChangesAsync();
        }
    }
}
