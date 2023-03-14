namespace MassTransit.ScopeSendingFilter.Web;

public class MassTransitOptions
{
    public const string Position = "MassTransit";
    public RabbitMq RabbitMq { get; set; } = null!;
}

public class RabbitMq
{
    public string VirtualHost { get; set; } = null!;
    public string Host { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ushort Port { get; set; }
    public bool UseAmqps { get; set; } = true;
}
