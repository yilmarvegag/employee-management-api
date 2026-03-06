using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.Interfaces
{
    /// <summary>
    /// Defines a contract for generating authentication tokens based on user credentials and role information.
    /// </summary>
    /// <remarks>Implementations of this interface are responsible for creating tokens that can be used to
    /// authenticate and authorize users within an application. The generated token typically encodes the provided
    /// username and role, and may include additional claims or metadata as required by the authentication
    /// system.</remarks>
    public interface ITokenService
    {
        TokenResult GenerateToken(string username, string role);
    }
}
