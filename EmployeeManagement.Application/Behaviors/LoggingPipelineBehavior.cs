using EmployeeManagement.Domain.Common.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(
            ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestId = Guid.NewGuid().ToString()[..8];

            _logger.LogInformation(
                "Starting request {@RequestName},{@DateTimeUtc}",
                requestId,
                requestName,
                DateTime.UtcNow.ToString("HH:mm:ss.fff"));

            var result = await next();

            if (result.IsFailure)
            {
                _logger.LogError(
                    "Request failure {@RequestName},{@Error}, {@ErrorDetail}, {@DateTimeUtc}",
                    result.Message,
                    result.Detail,
                    requestId,
                    requestName,
                    DateTime.UtcNow.ToString("HH:mm:ss.fff"));
            }

            _logger.LogInformation(
                "Completed request {@RequestName},{@DateTimeUtc}",
                requestId,
                requestName,
                DateTime.UtcNow.ToString("HH:mm:ss.fff"));

            return result;
        }
    }
}
