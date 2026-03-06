using EmployeeManagement.Domain.Common.Wrappers;
using MediatR;

namespace EmployeeManagement.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result> where TQuery : IQuery
    {
    }
}
