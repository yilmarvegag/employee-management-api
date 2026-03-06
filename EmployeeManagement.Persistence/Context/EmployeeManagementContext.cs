using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Context
{
    public class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<User> Users => Set<User>();
        public DbSet<PositionHistory> PositionHistories => Set<PositionHistory>();
        public DbSet<EmployeeProject> EmployeeProjects  => Set<EmployeeProject>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeManagementContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
