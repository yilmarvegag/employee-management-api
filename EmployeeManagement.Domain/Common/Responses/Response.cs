using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Domain.Common.Responses
{
    /// <summary>
    /// Represents a standardized response that includes a message, optional data payload, and a collection of errors.
    /// </summary>
    /// <remarks>This class is commonly used to encapsulate the result of an operation, providing both success
    /// and error information in a consistent format. It inherits from <see cref="ProblemDetails"/>, allowing
    /// integration with standardized error reporting in web APIs. The <see cref="Message"/> property can be used to
    /// convey additional context or status information, while <see cref="Errors"/> provides detailed error descriptions
    /// when applicable.</remarks>
    /// <typeparam name="T">The type of the data payload included in the response.</typeparam>
    public class Response<T> : ProblemDetails
    {
        public string Message { get; set; } = default!;

        public T? Data { get; set; }

        public List<Error> Errors { get; set; } = [];
        
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string TraceId { get; set; }

        public Response()
        {
        }

        public Response(T data)
        {
            Data = data;
        }
    }

}
