using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitcoinApp.Infrastructure.MassTransit;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureMassTransit(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        string? messaging = builder.Configuration.GetConnectionString("messaging");

        Console.WriteLine("Configuring MassTransit");
        Console.WriteLine(messaging);

        builder.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumers(typeof(Extensions).Assembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                Console.WriteLine("RabbitMQ is being used");
                Console.WriteLine(messaging);

                cfg.Host(new Uri(messaging), c => { });
                cfg.ConfigureEndpoints(context);
            });
        });

        return builder;
    }
}
