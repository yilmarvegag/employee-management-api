using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces.Repositories
{
    /// <summary>
    /// Defines a contract for retrieving user information by username asynchronously.
    /// </summary>
    /// <remarks>Implementations of this interface should provide thread-safe access to user data sources.
    /// Methods may return <see langword="null"/> if the specified user does not exist.</remarks>
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}
