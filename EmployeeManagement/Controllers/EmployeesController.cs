using EmployeeManagement.API.Abstractions;
using EmployeeManagement.Application.Features.Commands.Employees.CreateEmployee;
using EmployeeManagement.Application.Features.Commands.Employees.DeleteEmployee;
using EmployeeManagement.Application.Features.Commands.Employees.UpdateEmployee;
using EmployeeManagement.Application.Features.Queries.Employees.GetEmployeeById;
using EmployeeManagement.Application.Features.Queries.Employees.GetEmployees;
using EmployeeManagement.Application.Features.Queries.Employees.GetEmployeesByDepartmentWithProjects;
using EmployeeManagement.Domain.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeesController : ApiController
    {
        public EmployeesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetEmployeesQuery());

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            GetEmployeeByIdQuery query = new(id);
            var result = await _mediator.Send(query, cancellationToken);

            return HandleResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateEmployeeCommand command)
        {
            if (id != command.Id)
                return HandleResult(Result.Failure("Id not found"));

            var result = await _mediator.Send(command);

            return HandleResult(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));

            return HandleResult(result);
        }

        [HttpGet("department/{departmentId}/projects")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeesByDepartmentWithProjects(Guid departmentId)
        {
            var query = new GetEmployeesByDepartmentWithProjectsQuery(departmentId);

            var result = await _mediator.Send(query);

            return HandleResult(result);
        }
    }
}
