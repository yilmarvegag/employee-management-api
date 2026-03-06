using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Context;

namespace EmployeeManagement.Persistence.Seeds
{
    public static class DepartmentSeed
    {
        public static async Task SeedAsync(EmployeeManagementContext context)
        {
            if (context.Departments.Any())
                return;

            var departments = new List<Department>
            {
                new("Human Resources"),
                new("Finance"),
                new("TI"),
                new("Marketing")
            };

            await context.Departments.AddRangeAsync(departments);

            await context.SaveChangesAsync();
        }
    }
}
