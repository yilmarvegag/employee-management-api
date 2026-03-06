namespace EmployeeManagement.Domain.Exceptions
{
    public class EmployeeManagementDomainException : Exception
    {
        public EmployeeManagementDomainException(string message)
            : base(message)
        {
        }
    }
}
