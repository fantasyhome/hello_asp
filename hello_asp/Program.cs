var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run (async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    
    await context.Response.WriteAsync($"<p>path:{path}</p>");
    await context.Response.WriteAsync($"<p>method:{method}</p>");
});

app.Run();