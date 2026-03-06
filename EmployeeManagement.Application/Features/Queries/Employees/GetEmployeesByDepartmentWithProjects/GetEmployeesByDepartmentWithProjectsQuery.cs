using EmployeeManagement.Application.Abstractions.Messaging;

namespace EmployeeManagement.Application.Features.Queries.Employees.GetEmployeesByDepartmentWithProjects
{
    public sealed record GetEmployeesByDepartmentWithProjectsQuery(Guid DepartmentId) : IQuery;
}
