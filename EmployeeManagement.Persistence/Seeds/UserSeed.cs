using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Context;

namespace EmployeeManagement.Persistence.Seeds
{
    public static class UserSeed
    {
        public static async Task SeedAsync(EmployeeManagementContext context)
        {
            if (context.Users.Any())
                return;

            var users = new List<User>
            {
                new("admin@employeemanagement.com",BCrypt.Net.BCrypt.HashPassword("Admin123!"),"Admin"),
                new("user@employeemanagement.com",BCrypt.Net.BCrypt.HashPassword("User123!"),"User")
            };

            await context.Users.AddRangeAsync(users);

            await context.SaveChangesAsync();
        }
    }
}
