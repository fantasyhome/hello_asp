namespace hello_asp;

public class MyMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("Hello Middleware Class!\n");
        await next(context);
        await context.Response.WriteAsync("Bye Middleware Class!\n");
    }
}

public static class MyMiddlewareExtensions
{
    // this indicates that this is an extension method
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MyMiddleware>();
    }
}