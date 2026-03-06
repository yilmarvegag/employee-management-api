using EmployeeManagement.Domain.Common;

namespace EmployeeManagement.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; private set; }

        private readonly List<Employee> _employees = [];

        public IReadOnlyCollection<Employee> Employees => _employees;

        private Department() { }

        public Department(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
