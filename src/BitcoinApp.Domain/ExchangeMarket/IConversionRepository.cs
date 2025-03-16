namespace BitcoinApp.Domain.ExchangeMarket;

public interface IExchangeMarketRepository
{
    Task AddAsync(Conversion conversion, CancellationToken cancellationToken = default);
    Task<Conversion> GetLatestConversion(CancellationToken cancellationToken = default);
}
