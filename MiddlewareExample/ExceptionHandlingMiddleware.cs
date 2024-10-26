namespace MiddlewareExample
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // Logic before calling the next middleware
                await next(context);
            }
            catch (Exception ex)
            {
                // Logic to handle exceptions (e.g., logging)
                _logger.LogError(ex, "An unhandled exception occurred.");

                // Custom response for the exception
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occurred while processing your request.");
            }

            // Logic after calling the next middleware (optional)
        }
    }
}
