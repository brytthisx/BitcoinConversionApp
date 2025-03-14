namespace BitcoinApp.Domain.ExchangeMarket;

public interface IConversionRepository
{
    Task AddAsync(Conversion conversion, CancellationToken cancellationToken = default);
    Task<Conversion> GetLatestConversion(CancellationToken cancellationToken = default);
}
