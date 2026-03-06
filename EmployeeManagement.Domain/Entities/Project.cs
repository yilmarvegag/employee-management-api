using EmployeeManagement.Domain.Common;

namespace EmployeeManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; private set; }

        private readonly List<EmployeeProject> _employeeProjects = [];

        public IReadOnlyCollection<EmployeeProject> EmployeeProjects => _employeeProjects;

        private Project() { }

        public Project(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
