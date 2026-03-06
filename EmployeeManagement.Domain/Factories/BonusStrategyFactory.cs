using EmployeeManagement.Domain.Strategies;

namespace EmployeeManagement.Domain.Factories
{
    public class BonusStrategyFactory : IBonusStrategyFactory
    {
        public IBonusStrategy Create(string role)
        {
            return role switch
            {
                "Manager" => new ManagerBonusStrategy(),
                _ => new RegularEmployeeBonusStrategy()
            };
        }
    }
}
