using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Common.Responses;
using EmployeeManagement.Domain.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.API.Abstractions
{
    /// <summary>
    /// Provides a base class for API controllers that supports standardized response handling and dependency injection
    /// for mediator and unit of work services.
    /// </summary>
    /// <remarks>ApiController is intended to be inherited by application-specific controllers to streamline
    /// API response formatting and facilitate integration with the mediator pattern and unit of work abstractions.
    /// Protected constructors allow derived controllers to inject required services. The class includes a protected
    /// method for generating consistent HTTP responses based on operation results. This base class does not implement
    /// any request handling logic directly.</remarks>
    public abstract class ApiController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IUnitOfWork _unitOfWork;

        protected ApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected ApiController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        protected IActionResult HandleResult(Result result)
        {
            Response<Object> response = new()
            {
                Instance = HttpContext.Request.Path,
                Message = result.Message
            };

            if (result.IsSuccess)
            {
                response.Title = HttpStatusCode.OK.ToString();
                response.Status = (int)HttpStatusCode.OK;
                response.Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200";
                response.Detail = result.Detail;
                response.Data = result.Value;

                return Ok(response);
            }

            response.Title = HttpStatusCode.BadRequest.ToString();
            response.Status = (int)HttpStatusCode.BadRequest;
            response.Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/400";
            return BadRequest(response);
        }
    }
}
