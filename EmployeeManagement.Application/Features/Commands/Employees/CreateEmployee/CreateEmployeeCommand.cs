using EmployeeManagement.Application.Abstractions.Messaging;

namespace EmployeeManagement.Application.Features.Commands.Employees.CreateEmployee
{
    public sealed record CreateEmployeeCommand(
        string Name,
        int CurrentPosition,
        decimal Salary,
        Guid DepartmentId
    ) : ICommand;
}
