using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.Strategies
{
    public class RegularEmployeeBonusStrategy : IBonusStrategy
    {
        public bool CanHandle(PositionType position)
        {
            return position == PositionType.RegularEmployee;
        }

        public decimal Calculate(decimal salary)
        {
            return salary * 0.10m;
        }
    }
}
