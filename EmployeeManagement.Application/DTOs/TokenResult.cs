namespace EmployeeManagement.Application.DTOs
{
    public class TokenResult
    {
        public string AccessToken { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
