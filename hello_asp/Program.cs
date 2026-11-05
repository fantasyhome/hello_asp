using hello_asp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyMiddleware>();
builder.Services.AddControllers(); // adds all the controller classes which name end with "controller"
var app = builder.Build();
app.MapControllers(); // map all methods in controllers
app.UseStaticFiles();

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

app.Map("number/{id?}", async context =>
{
    string? id = context.Request.RouteValues["id"]?.ToString();

    await context.Response.WriteAsync($"ID: {id}\n");
});

app.Map("number/{date:datetime}", async context =>
{
    DateTime date = Convert.ToDateTime(context.Request.RouteValues["date"]);

    await context.Response.WriteAsync($"date: {date}\n");
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
    Console.WriteLine("first middleware");
    await next(context);
});

// app.UseMiddleware<MyMiddleware>();
app.UseMyMiddleware(); // syntactic sugar, equal to MyMiddlewareExtensions.UseMyMiddleware(app);

app.Run();