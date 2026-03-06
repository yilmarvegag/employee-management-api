using EmployeeManagement.Application.Abstractions.Messaging;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Common.Wrappers;

namespace EmployeeManagement.Application.Features.Commands.Employees.DeleteEmployee
{
    internal sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteEmployeeCommandHandler(
            IEmployeeRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (employee == null)
                return Result.Failure("Employee not found");

            _repository.Delete(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Employee deleted successfully");
        }
    }
}
