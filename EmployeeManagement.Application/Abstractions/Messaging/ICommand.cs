using EmployeeManagement.Domain.Common.Wrappers;
using MediatR;

namespace EmployeeManagement.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>
    {
    }
}
