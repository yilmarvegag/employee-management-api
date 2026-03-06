using EmployeeManagement.Application.Abstractions.Messaging;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Common.Wrappers;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Application.Features.Commands.Employees.CreateEmployee
{
    internal sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(
            IEmployeeRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(
                request.Name,
                (PositionType)request.CurrentPosition,
                request.Salary,
                request.DepartmentId
            );

            await _repository.AddAsync(employee, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Employee created successfully", employee.Id);
        }
    }
}
