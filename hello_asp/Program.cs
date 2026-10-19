using hello_asp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyMiddleware>();
var app = builder.Build();

app.UseWhen(
    context => context.Request.Query.ContainsKey("hello"),
    appwhen =>
    {
        appwhen.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("world!\n");
            await next();
        });
    }
);

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("first middleware\n");
    await next(context);
});

// app.UseMiddleware<MyMiddleware>();
app.UseMyMiddleware(); // syntactic sugar, equal to MyMiddlewareExtensions.UseMyMiddleware(app);

app.Run (async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    
    await context.Response.WriteAsync($"<p>path:{path}</p>");
    await context.Response.WriteAsync($"<p>method:{method}</p>\n");
});

app.Run();