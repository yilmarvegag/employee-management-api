using EmployeeManagement.API.Abstractions;
using EmployeeManagement.Application.Features.Commands.Secutiry.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ApiController
    {
        public AuthController(IMediator mediator)
        : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }
    }
}
