using MiddlewareExample;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseMiddleware<RequestTimingMiddleware>();
// Use the custom middleware via the extension method
app.UseExceptionHandlingMiddleware();
app.UseWhen(
  context => context.Request.Path.StartsWithSegments("/admin"),
  app =>
  {
      // Middleware for additional security checks in the admin section
      app.Use(async (context, next) =>
      {
          // Example security check (you could validate specific headers, etc.)
          //if (!context.User.Identity.IsAuthenticated || !context.User.IsInRole("Admin"))
          //{
          //    // Return unauthorized if the user is not an admin
          //    context.Response.StatusCode = StatusCodes.Status403Forbidden;
          //    await context.Response.WriteAsync("Access Denied");
          //    return;
          //}

          await next();
      });
  }
);

app.Use(async (HttpContext context, Func<Task> next) => {
    await context.Response.WriteAsync("Hello Manash");
    await next();
});

app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Hello again Manash ");
});

app.Run();


// First middleware (non-terminal, continues to next middleware)
app.Use(async (HttpContext context, Func<Task> next) =>
{
    await context.Response.WriteAsync("Hello Manash\n");
    //await next(); // This allows the next middleware to run
   // return;
});

// Terminal middleware (no next middleware will be executed after this)
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello again Manash\n");
});

app.Run();