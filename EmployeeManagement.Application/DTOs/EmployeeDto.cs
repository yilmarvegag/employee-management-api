namespace EmployeeManagement.Application.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string CurrentPosition { get; set; }

        public decimal Salary { get; set; }

    }
}
