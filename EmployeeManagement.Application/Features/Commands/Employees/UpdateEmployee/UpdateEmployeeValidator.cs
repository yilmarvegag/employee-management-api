using FluentValidation;

namespace EmployeeManagement.Application.Features.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Salary)
                .GreaterThan(0);

            RuleFor(x => x.CurrentPosition)
                .GreaterThan(0);
        }
    }
}
