namespace EmployeeManagement.Application.DTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public DateTime ExpiresAt { get; set; }

        public string Role { get; set; }

        public string Username { get; set; }
    }
}
