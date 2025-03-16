using BitcoinApp.Application.Shared;
using BitcoinApp.Domain;
using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Domain.Services;
using BitcoinApp.Infrastructure.Events;
using BitcoinApp.Infrastructure.Exceptions;
using BitcoinApp.Infrastructure.Persistence;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using BitcoinApp.Infrastructure.ReadServices;
using BitcoinApp.Infrastructure.Services;
using BitcoinApp.Infrastructure.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using BitcoinApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using BitcoinApp.Application.Crypto.GetLatestConversion;
using BitcoinApp.Application.CryptoHistory.GetCryptoHistory;
using BitcoinApp.Application.CryptoHistory.CreateCryptoHistory;
using BitcoinApp.Application.CryptoHistory.UpdateCryptoHistory;
using BitcoinApp.Application.CryptoHistory.DeleteCryptoHistory;

namespace BitcoinApp.Infrastructure.DependencyInjection;

public static class Extensions
{
    public static void ConfigureDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.BindDbContext<AppDbContext>();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetLatestConversionQueryHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetCryptoHistoryRecordsQueryHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateCryptoHistoryCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(UpdateCryptoHistoryCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(DeleteCryptoHistoryCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetLatestConversionQueryHandler).Assembly);
        });

        builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        builder.Services.AddScoped<ICryptoHistoryRecordRepository, CryptoHistoryRecordRepository>();
        builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        builder.Services.AddSingleton<EventMapperFactory>(provider =>
        {
            Dictionary<Type, IEventMapper> mappers = new()
            {
            };

            return new EventMapperFactory(mappers);
        });

        builder.Services.AddValidatorsFromAssemblyContaining<IApplicationValidator>(ServiceLifetime.Transient);
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<CommandValidationExceptionHandler>();
        builder.Services.AddScoped<ICryptoHistoryReadService, CryptoHistoryReadService>();
        builder.Services.AddScoped<IConversionReadService, ConversionReadService>();
        builder.Services.AddScoped<IExchangeConversionService, ExchangeConversionService>();

        builder.Services.AddHttpClient<IBitcoinToEurPriceService, BitcoinToEurPriceService>();

        builder.Services.AddScoped<CryptoHistoryDomainService>();
    }
}