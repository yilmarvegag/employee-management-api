using EmployeeManagement.Domain.Common;

namespace EmployeeManagement.Domain.Entities
{
    public class PositionHistory : BaseEntity
    {
        public Guid EmployeeId { get; private set; }

        public string Position { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        private PositionHistory() { }

        public PositionHistory(Guid employeeId, string position, DateTime startDate)
        {
            EmployeeId = employeeId;
            Position = position;
            StartDate = startDate;
        }
    }
}