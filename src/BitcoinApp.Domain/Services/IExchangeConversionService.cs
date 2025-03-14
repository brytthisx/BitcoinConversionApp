namespace BitcoinApp.Domain.Services;

public interface IExchangeConversionService
{
    Task<decimal> GetExchangeConversionRateAsync(string fromCurrency, string toCurrency);
    Task<decimal> GetLatestConversionRateToCZKAsync(string targetCurrency);
}
