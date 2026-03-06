

using EmployeeManagement.Application.Abstractions.Messaging;

namespace EmployeeManagement.Application.Features.Commands.Secutiry.Login
{
    public sealed record LoginCommand(string Username, string Password) : ICommand;
}
