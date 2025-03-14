using BitcoinApp.Application.Shared;
using BitcoinApp.Domain;
using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Domain.Services;
using BitcoinApp.Infrastructure.Events;
using BitcoinApp.Infrastructure.Exceptions;
using BitcoinApp.Infrastructure.Persistence;
using BitcoinApp.Infrastructure.Persistence.Database.Configuration.Domain.HistoryRecords;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using BitcoinApp.Infrastructure.ReadServices;
using BitcoinApp.Infrastructure.Services;
using BitcoinApp.Infrastructure.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BitcoinApp.Infrastructure.DependencyInjection;

public static class Extensions
{
    public static void ConfigureDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.BindDbContext<AppDbContext>();

        builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        builder.Services.AddScoped<ICryptoHistoryRepository, CryptoHistoryRecordRepository>();
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
