using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.Strategies
{
    public class BonusStrategyFactory
    {
        private static readonly List<IBonusStrategy> Strategies =
        [
            new ManagerBonusStrategy(),
            new RegularEmployeeBonusStrategy()
        ];

        public static IBonusStrategy Create(PositionType position)
        {
            return Strategies.First(x => x.CanHandle(position));
        }
    }
}
