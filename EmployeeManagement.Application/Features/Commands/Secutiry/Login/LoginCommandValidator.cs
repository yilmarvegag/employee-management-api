using FluentValidation;

namespace EmployeeManagement.Application.Features.Commands.Secutiry.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .EmailAddress()
                .WithMessage("Username must be a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(20)
                .WithMessage("Password cannot exceed 20 characters.");
        }
    }
}
