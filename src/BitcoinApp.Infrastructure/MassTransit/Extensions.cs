using BitcoinApp.Application.CryptoHistory.CreateCryptoHistory;
using BitcoinApp.Domain;
using MassTransit;
using MassTransit.Internals;
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
        builder.Services.AddMediator(cfg =>
        {
            AddMediatorConsumersFromAssembly(cfg);

            cfg.ConfigureMediator((context, cfg) =>
            {
                cfg.UseConsumeFilter(typeof(EventsFilter<>), context,
                   x => x.Include(type => !type.HasInterface<IDomainEvent>()));
            });

            builder.Services.AddMassTransit(x =>
            {
                //below Consumers for RabbitMq

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    Console.WriteLine("RabbitMq is being used");
                    Console.WriteLine(messaging);
                    cfg.Host(new Uri(messaging), c => { });

                    cfg.ConfigureEndpoints(context);
                });
            });
        });
        return builder;
    }

    private static void AddMediatorConsumersFromAssembly(IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddConsumers(typeof(CreateCryptoHistoryCommandHandler).Assembly);
    }
}
