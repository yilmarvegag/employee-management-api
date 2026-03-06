using EmployeeManagement.Domain.Common.Wrappers;
using MediatR;

namespace EmployeeManagement.Application.Abstractions.Messaging
{
    /// <summary>
    /// Result es el tipo de retorno esperado
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand
    {
    }
}
