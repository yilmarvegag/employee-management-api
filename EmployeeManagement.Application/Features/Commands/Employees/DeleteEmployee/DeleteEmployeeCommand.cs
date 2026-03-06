using EmployeeManagement.Application.Abstractions.Messaging;

namespace EmployeeManagement.Application.Features.Commands.Employees.DeleteEmployee
{
    public sealed record DeleteEmployeeCommand(Guid Id) : ICommand;
}
