namespace hello_asp;

public class MyMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        await context.Response.WriteAsync("Hello Middleware Class!\n");

        // 只注册post /map2 且未注册MapFallback时, get /map2会吞掉返回流程导致丢失Bye Middleware Class!
        // 添加try finally可修复,问了gpt很久,大概原因:
        // 未匹配 Endpoint 后的默认处理流程可能尝试修改已经开始发送的响应(await WriteAsync();)，导致异常。
        // try-finally 让 Bye 在异常退出时仍有机会执行，但并不意味着这个请求的整个响应过程已经正常完成。
        try
        {
            await next(context);
        }
        finally
        {
            await context.Response.WriteAsync("Bye Middleware Class!\n");
        }
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