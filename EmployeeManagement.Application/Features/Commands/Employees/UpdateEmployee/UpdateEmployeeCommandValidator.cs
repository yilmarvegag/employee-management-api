using EmployeeManagement.Domain.Enums;
using FluentValidation;

namespace EmployeeManagement.Application.Features.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Salary)
                .GreaterThan(0);

            RuleFor(x => x.CurrentPosition)
                .Must(value => Enum.IsDefined(typeof(PositionType), value))
                .WithMessage("Invalid employee position.");
        }
    }
}
