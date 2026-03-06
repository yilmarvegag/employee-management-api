using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces.Repositories
{
    /// <summary>
    /// Defines a contract for managing employee entities in a data store, providing asynchronous methods for
    /// retrieving, adding, and querying employees.
    /// </summary>
    /// <remarks>This interface extends <see cref="IGenericRepository{Employee}"/>, adding employee-specific
    /// operations. Implementations should ensure thread safety for concurrent access if used in multi-threaded
    /// scenarios. Avoid DRY</remarks>
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<EmployeeWithProjectsDto>> GetByDepartmentWithProjects(Guid DepartmentId, CancellationToken cancellationToken);
        Task AddAsync(Employee employee, CancellationToken cancellationToken);
    }
}
