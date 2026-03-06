using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Salary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.CurrentPosition)
                .HasConversion<int>()
                .IsRequired();

            builder.HasMany(e => e.PositionHistories)
                .WithOne()
                .HasForeignKey(ph => ph.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
