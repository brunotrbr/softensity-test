using AccessControl.Application.UseCases.v1;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.ExceptionHandlers
{
    public class GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GeneralExceptionHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Type = nameof(GeneralExceptionHandler),
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred while processing your request."
            };

            _logger.LogError(exception, exception.Message);

            context.Response.StatusCode = problemDetails.Status.Value;

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
