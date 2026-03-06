using EmployeeManagement.Domain.Common;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Domain.Strategies;

namespace EmployeeManagement.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; private set; }

        public PositionType CurrentPosition { get; private set; }

        public decimal Salary { get; private set; }

        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }

        private readonly List<EmployeeProject> _employeeProjects = [];
        public IReadOnlyCollection<EmployeeProject> EmployeeProjects => _employeeProjects;

        private readonly List<PositionHistory> _positionHistories = [];

        public IReadOnlyCollection<PositionHistory> PositionHistories => _positionHistories;

        protected Employee() { }

        public Employee(string name, PositionType position, decimal salary, Guid departmentId)
        {
            Id = Guid.NewGuid();
            Name = name;
            CurrentPosition = position;
            Salary = salary;
            DepartmentId = departmentId;
        }

        public void Update(string name, PositionType position, decimal salary)
        {
            Name = name;
            CurrentPosition = position;
            Salary = salary;
        }

        public decimal CalculateYearlyBonus()
        {
            var strategy = BonusStrategyFactory.Create(CurrentPosition);
            return strategy.Calculate(Salary);
        }
    }
}
