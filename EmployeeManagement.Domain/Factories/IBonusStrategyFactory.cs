using EmployeeManagement.Domain.Strategies;

namespace EmployeeManagement.Domain.Factories
{
    public interface IBonusStrategyFactory
    {
        IBonusStrategy Create(string role);
    }
}
