namespace EmployeeManagement.Application.DTOs
{
    public class EmployeeWithProjectsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }

        public string Position { get; set; }

        public List<string> Projects { get; set; } = new();
    }
}
