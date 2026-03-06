using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.Strategies
{
    public interface IBonusStrategy
    {
        bool CanHandle(PositionType position);

        decimal Calculate(decimal salary);
    }
}
