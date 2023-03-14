namespace MassTransit.ScopeSendingFilter.Web.Services;

public interface IMyContextService
{
    public Guid? Id { get; }

    public void Fill(Guid id);
}

public class MyContextService: IMyContextService
{
    public Guid? Id { get; private set; } = null;

    public void Fill(Guid id)
    {
        Id = id;
    }
}