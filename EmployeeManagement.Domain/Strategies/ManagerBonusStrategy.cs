using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.Strategies
{
    public class ManagerBonusStrategy : IBonusStrategy
    {
        public bool CanHandle(PositionType position)
        {
            return position == PositionType.Manager
                || position == PositionType.SeniorManager
                || position == PositionType.Director;
        }

        public decimal Calculate(decimal salary)
        {
            return salary * 0.20m;
        }
    }
}
