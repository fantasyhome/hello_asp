namespace hello_asp;

public class MyMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        // await context.Response.WriteAsync("Hello Middleware Class!\n"); 不仅写入响应正文，还会导致响应开始发送,后续如果再写入会报错

        Console.WriteLine("Before Request");

        await next(context);

        Console.WriteLine("After Request");

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