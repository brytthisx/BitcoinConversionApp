namespace BitcoinApp.Domain.Services;

public interface IBitcoinToEurPriceService
{
    Task<(decimal Price, string TargetCurrency, DateTime DateOfRecord)> GetLatestBtcEurPriceAsync();
}
