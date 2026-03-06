namespace EmployeeManagement.Domain.Common.Responses
{
    public class Error
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public Error(string key, string message)
        {
            Field = key;
            Message = message;
        }
    }
}
