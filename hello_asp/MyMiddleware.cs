namespace hello_asp;

public class MyMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("Hello Middleware Class!");
        await next(context);
        await context.Response.WriteAsync("Bye Middleware Class!");
    }
}