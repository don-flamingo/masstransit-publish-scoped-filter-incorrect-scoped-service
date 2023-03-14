using MassTransit.ScopeSendingFilter.Web.Services;

namespace MassTransit.ScopeSendingFilter.Web.Filters;

public class ReproductionPublishFilter<T> : IFilter<PublishContext<T>>
    where T : class
{
    private readonly IMyContextService _contextService;

    public ReproductionPublishFilter(IMyContextService contextService)
    {
        _contextService = contextService;
    }
    
    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        Console.WriteLine("Filter: Scoped service has a value of: " + _contextService.Id);

        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}