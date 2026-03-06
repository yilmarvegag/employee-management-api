using EmployeeManagement.Application.Abstractions.Messaging;

namespace EmployeeManagement.Application.Features.Queries.Employees.GetEmployeeById
{
    public sealed record GetEmployeeByIdQuery(Guid Id) : IQuery;
}
