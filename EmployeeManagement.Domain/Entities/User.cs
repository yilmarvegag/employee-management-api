using EmployeeManagement.Domain.Common;

namespace EmployeeManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; private set; }

        public string PasswordHash { get; private set; }

        public string Role { get; private set; }

        private User() { }

        public User(string username, string passwordHash, string role)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
