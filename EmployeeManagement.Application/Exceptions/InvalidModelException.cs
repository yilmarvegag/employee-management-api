using EmployeeManagement.Domain.Common.Responses;
using EmployeeManagement.Domain.Exceptions;
using FluentValidation.Results;

namespace EmployeeManagement.Application.Exceptions
{
    public sealed class InvalidModelException : EmployeeManagementDomainException
    {
        public List<Error> Errors { get; set; } = new List<Error>();

        public InvalidModelException() : base("One or more validation failures have occurred.")
        {
        }

        public InvalidModelException(string message) : base(message)
        {
        }

        public InvalidModelException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var error in failures)
            {
                Errors.Add(new Error(error.PropertyName, error.ErrorMessage));
            }
        }
    }
}
