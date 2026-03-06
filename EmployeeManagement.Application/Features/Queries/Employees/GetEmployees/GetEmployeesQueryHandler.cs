using EmployeeManagement.Application.Abstractions.Messaging;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces.Repositories;
using AutoMapper;
using EmployeeManagement.Domain.Common.Wrappers;

namespace EmployeeManagement.Application.Features.Queries.Employees.GetEmployees
{
    internal sealed class GetEmployeesQueryHandler: IQueryHandler<GetEmployeesQuery>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        public GetEmployeesQueryHandler(
            IEmployeeRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            Result result = new();
            var employees = await _repository.GetAllAsync(cancellationToken);

            IEnumerable<EmployeeDto> mapped = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            result.Message = "Query successfully completed.";
            result.Value = mapped;

            return result;
        }

    }
}
