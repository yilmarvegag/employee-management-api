using EmployeeManagement.Application.Abstractions.Messaging;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Common.Wrappers;

namespace EmployeeManagement.Application.Features.Commands.Secutiry.Login
{
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(
            IUserRepository repository,
            IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository
            .GetByUsernameAsync(request.Username);

            if (user == null)
                return Result.Failure("Invalid credentials");

            var validPassword = BCrypt.Net.BCrypt
                .Verify(request.Password, user.PasswordHash);

            if (!validPassword)
                return Result.Failure("Invalid credentials");

            var token = _tokenService
                .GenerateToken(user.Username, user.Role);

            var response = new LoginResponse
            {
                AccessToken = token.AccessToken,
                ExpiresAt = token.ExpiresAt,
                Role = user.Role,
                Username = user.Username
            };

            return Result.Success("Login successful", response);
        }
    }
}
