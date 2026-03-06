namespace EmployeeManagement.Domain.Common.Wrappers
{
    public class Result
    {
        public bool IsSuccess { get; } = true;
        public bool IsFailure => !IsSuccess;
        public string Message { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public Object Value { get; set; } = default!;

        public Result()
        {
        }
        

        private Result(bool isSuccess, string message = "", Object value = null, string detail = "")
        {
            IsSuccess = isSuccess;
            Message = message;
            Value = value;
            Detail = detail;
        }

        
        public static Result Success(string message = "", Object value = null)
        {
            return new Result(true, message, value);
        }

        public static Result Failure(string message, string detail = "")
        {
            return new Result(false, message, null, detail);
        }

        public static Result Failure(string message, Object value, string detail = "")
        {
            return new Result(false, message, value, detail);
        }
    }
}
