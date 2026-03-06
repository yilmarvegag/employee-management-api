using EmployeeManagement.Application.Abstractions.Messaging;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Common.Wrappers;
using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Application.Features.Commands.Employees.UpdateEmployee
{
    internal sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateEmployeeCommandHandler(
            IEmployeeRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (employee == null)
                return Result.Failure("Employee not found");

            employee.Update(
                request.Name,
                (PositionType)request.CurrentPosition,
                request.Salary);

            _repository.Update(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Employee updated successfully");
        }
    }
}
