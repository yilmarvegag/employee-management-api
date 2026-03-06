namespace EmployeeManagement.Domain.Entities
{
    public class EmployeeProject
    {
        public Guid EmployeeId { get; set; }

        public Guid ProjectId { get; set; }

        public Employee Employee { get; set; }

        public Project Project { get; set; }
    }
}
