using BitcoinApp.Application.Crypto.GetLatestConversion;
using BitcoinApp.Application.Shared;
using BitcoinApp.Domain.ExchangeMarket;
using BitcoinApp.Domain.Services;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.ReadServices;

public class ConversionReadService(
    AppDbContext dbContext,
    IBitcoinToEurPriceService bitcoinToEurPriceService,
    IExchangeConversionService exchangeConversionService)
    : IConversionReadService
{
    public IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken)
        where T : class
    {
        return dbContext.Set<T>()
            .FromSqlRaw(sql, parameters)
            .AsNoTracking();
    }

    public async Task<GetLatestConversionDto> GetLatestConversion(CancellationToken cancellationToken)
    {
        (decimal Price, string TargetCurrency, DateTime DateOfRecord) latestPrice =
            await bitcoinToEurPriceService.GetLatestBtcEurPriceAsync();
        decimal latestConversionRate = await exchangeConversionService.GetExchangeConversionRateAsync("EUR", "CZK");
        return new GetLatestConversionDto
        (
            new Money(latestPrice.Price, latestPrice.TargetCurrency),
            new Money(latestPrice.Price * latestConversionRate, "CZK"),
            latestPrice.DateOfRecord
        );
    }
}
