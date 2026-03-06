using FluentValidation;

namespace EmployeeManagement.Application.Features.Commands.Employees.CreateEmployee
{
    public class UpdateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Employee name is required.")
                .MaximumLength(150)
                .WithMessage("Employee name must not exceed 150 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .WithMessage("Salary must be greater than zero.");

            RuleFor(x => x.CurrentPosition)
                .IsInEnum()
                .WithMessage("Invalid employee position.");

            RuleFor(x => x.DepartmentId)
                .NotNull()
                .WithMessage("Department Id is required")
                .NotEqual(Guid.Empty)
                .WithMessage("Department Id cannot be empty");
        }
    }
}
