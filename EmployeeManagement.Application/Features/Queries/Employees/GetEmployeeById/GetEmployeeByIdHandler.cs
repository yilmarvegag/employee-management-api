using AutoMapper;
using EmployeeManagement.Application.Abstractions.Messaging;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Common.Wrappers;

namespace EmployeeManagement.Application.Features.Queries.Employees.GetEmployeeById
{
    internal sealed class GetEmployeeByIdHandler : IQueryHandler<GetEmployeeByIdQuery>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        public GetEmployeeByIdHandler(
            IEmployeeRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (employee == null)
                return Result.Failure("Employee not found");

            var mapped = _mapper.Map<EmployeeDto>(employee);
            var result = new
            {
                mapped,
                Bonus = Math.Round(employee.CalculateYearlyBonus(), 2),
            };

            return Result.Success("Employee found", result);
        }
    }
}
