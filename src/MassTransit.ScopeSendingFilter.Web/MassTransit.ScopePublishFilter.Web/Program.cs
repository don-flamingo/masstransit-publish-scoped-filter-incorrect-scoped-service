using System.Security.Authentication;
using MassTransit;
using MassTransit.ScopeSendingFilter.Web;
using MassTransit.ScopeSendingFilter.Web.Filters;
using MassTransit.ScopeSendingFilter.Web.Middlewares;
using MassTransit.ScopeSendingFilter.Web.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .Configure<MassTransitOptions>(builder.Configuration.GetSection(MassTransitOptions.Position))
    .AddScoped<IMyContextService, MyContextService>()
    .AddMassTransit(configurator =>
    {
        configurator.UsingRabbitMq((context, rabbitConfigurator) =>
        {
            rabbitConfigurator.UseMessageScope(context);
            rabbitConfigurator.UsePublishFilter(typeof(ReproductionPublishFilter<>), context);

            var options = context.GetRequiredService<IOptions<MassTransitOptions>>().Value;

            rabbitConfigurator.Host(
                options.RabbitMq.Host,
                options.RabbitMq.Port,
                options.RabbitMq.VirtualHost,
                rabbitMqHostConfigure =>
                {
                    rabbitMqHostConfigure.Username(options.RabbitMq.User);
                    rabbitMqHostConfigure.Password(options.RabbitMq.Password);

                    if (options.RabbitMq.UseAmqps)
                    {
                        rabbitMqHostConfigure.UseSsl(ssl => ssl.Protocol = SslProtocols.Tls12);
                    }
                });
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<SetContextMiddleware>();

app.Run();