using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Seeds
{
    public static class EmployeeSeeder
    {
        public static async Task SeedAsync(EmployeeManagementContext context)
        {
            var department1 = await context.Departments.FirstAsync(d => d.Name == "Human Resources");
            var department2 = await context.Departments.FirstAsync(d => d.Name == "Finance");
            var department3 = await context.Departments.FirstAsync(d => d.Name == "TI");
            var department4 = await context.Departments.FirstAsync(d => d.Name == "Marketing");

            if (context.Employees.Any())
                return;
            var employees = new List<Employee>
            {
                new("John Doe", PositionType.Manager, 6000, department1.Id),
                new("Jane Smith", PositionType.RegularEmployee, 3500, department2.Id),
                new("Michael Brown", PositionType.SeniorManager, 8000, department3.Id),
                new("Kevin Muentes", PositionType.Director, 8000, department4.Id)
            };

            await context.Employees.AddRangeAsync(employees);
            await context.SaveChangesAsync();
        }
    }
}
