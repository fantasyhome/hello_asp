using hello_asp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyMiddleware>();
var app = builder.Build();

// Endpoints mapped via MapXxx are executed after the middleware pipeline.
app.Map("map1", async (context) =>
{
    await context.Response.WriteAsync("Map1\n");
});

app.Map("files/{filename}.{extension}", async context =>
{
    string? filename = context.Request.RouteValues["filename"]?.ToString();
    string? extension = context.Request.RouteValues["extension"]?.ToString();

    await context.Response.WriteAsync($"In file, {filename}.{extension}\n");
});

app.Map("id/{id?}", async context =>
{
    string? id = context.Request.RouteValues["id"]?.ToString();

    await context.Response.WriteAsync($"In file, {id}\n");
});

app.MapPost("map2", async (context) =>
{
    await context.Response.WriteAsync("Map2\n");
});

app.MapFallback(async context =>
{
    await context.Response.WriteAsync("Fallback\n");
});

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

// app.Run (async (HttpContext context) =>
// {
//     string path = context.Request.Path;
//     string method = context.Request.Method;
//     
//     await context.Response.WriteAsync($"<p>path:{path}</p>");
//     await context.Response.WriteAsync($"<p>method:{method}</p>\n");
// });

app.Run();