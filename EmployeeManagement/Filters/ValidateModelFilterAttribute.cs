using EmployeeManagement.Domain.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EmployeeManagement.API.Filters
{
    /// <summary>
    /// Before the controller action is executed. Validates at model binding level.
    /// </summary>
    public class ValidateModelFilterAttribute : IAsyncActionFilter
    {
        /// <summary>
        /// Create a bad request error based on model state.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                KeyValuePair<string, IEnumerable<string>?>[] errorsInModelState = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage)).ToArray();

                int httpStatusCode = (int)HttpStatusCode.UnprocessableEntity;
                string httpStatusMessage = HttpStatusCode.UnprocessableEntity.ToString();
                Response<Object> response = new()
                {
                    Message = httpStatusMessage,
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/422",
                    Title = httpStatusMessage,
                    Status = httpStatusCode,
                    Instance = context.HttpContext.Request.Path,
                    TraceId = context.HttpContext.TraceIdentifier
                };

                foreach (KeyValuePair<string, IEnumerable<string>?> error in errorsInModelState)
                {
                    foreach (string subError in error.Value ?? Enumerable.Empty<string>())
                    {
                        Error errorModel = new(error.Key, subError);
                        response.Errors.Add(errorModel);
                    }
                }
                context.Result = new UnprocessableEntityObjectResult(response);
                return;
            }
            //Call the next delegate/middleware in the pipeline.
            await next();
        }
    }
}
