var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Hello Manash");
});

app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Hello again Manash ");
});

app.Run();


// First middleware (non-terminal, continues to next middleware)
app.Use(async (HttpContext context, Func<Task> next) => {
    await context.Response.WriteAsync("Hello Manash\n");
    await next(); // This allows the next middleware to run
});

// Terminal middleware (no next middleware will be executed after this)
app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Hello again Manash\n");
});

app.Run();