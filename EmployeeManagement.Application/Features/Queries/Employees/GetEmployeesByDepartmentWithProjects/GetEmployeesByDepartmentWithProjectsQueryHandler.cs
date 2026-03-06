using AutoMapper;
using EmployeeManagement.Application.Abstractions.Messaging;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Common.Wrappers;

namespace EmployeeManagement.Application.Features.Queries.Employees.GetEmployeesByDepartmentWithProjects
{
    internal sealed class GetEmployeesByDepartmentWithProjectsQueryHandler : IQueryHandler<GetEmployeesByDepartmentWithProjectsQuery>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        public GetEmployeesByDepartmentWithProjectsQueryHandler(
            IEmployeeRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetEmployeesByDepartmentWithProjectsQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repository.GetByDepartmentWithProjects(request.DepartmentId, cancellationToken);

            return Result.Success("Employees retrieved successfully", employees);
        }
    }
}
