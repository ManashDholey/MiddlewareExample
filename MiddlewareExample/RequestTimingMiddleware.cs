using System.Diagnostics;

namespace MiddlewareExample
{
    public class RequestTimingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Log the start of the request
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);

            // Call the next middleware in the pipeline
            await next(context);

            // Log the completion of the request
            stopwatch.Stop();
            _logger.LogInformation("Finished handling request in {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
        }
    }
}
