using FluentValidation;

namespace EmployeeManagement.Application.Features.Commands.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Employee Id must be a valid GUID.");
        }
    }
}
