using EmployeeManagement.Application.Abstractions.Messaging;

namespace EmployeeManagement.Application.Features.Commands.Employees.UpdateEmployee
{
    public sealed record UpdateEmployeeCommand(Guid Id, string Name, int CurrentPosition, decimal Salary) : ICommand;
}
