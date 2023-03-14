using MassTransit.ScopeSendingFilter.Web.Services;

namespace MassTransit.ScopeSendingFilter.Web.Middlewares;

public class SetContextMiddleware
{
    private readonly RequestDelegate _next;

    public SetContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public Task Invoke(HttpContext httpContext, IMyContextService contextService)
    {
        contextService.Fill(Guid.NewGuid());

        return _next.Invoke(httpContext);
    }
}