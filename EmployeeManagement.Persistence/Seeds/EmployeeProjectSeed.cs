using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Seeds
{
    public static class EmployeeProjectSeed
    {
        public static async Task SeedAsync(EmployeeManagementContext context)
        {
            if (context.EmployeeProjects.Any())
                return;

            var employee1 = await context.Employees.FirstAsync();
            var employee2 = await context.Employees.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
            var project1 = await context.Projects.FirstAsync(e => e.Name == "ERP System");
            var project2 = await context.Projects.FirstAsync(e => e.Name == "Mobile Banking");
            var project3 = await context.Projects.FirstAsync(e => e.Name == "E-commerce Website");
            var project4 = await context.Projects.FirstAsync(e => e.Name == "Data Analytics Dashboard");

            var relation = new List<EmployeeProject>
            {
                new() { EmployeeId = employee1.Id, ProjectId = project1.Id },
                new() { EmployeeId = employee1.Id, ProjectId = project2.Id },
                new() { EmployeeId = employee1.Id, ProjectId = project3.Id },
                //employee2 is not assigned to any project
                new() { EmployeeId = employee2.Id, ProjectId = project2.Id },
                new() { EmployeeId = employee2.Id, ProjectId = project3.Id },
                new() { EmployeeId = employee2.Id, ProjectId = project4.Id },
            };

            await context.EmployeeProjects.AddRangeAsync(relation);

            await context.SaveChangesAsync();
        }
    }
}
