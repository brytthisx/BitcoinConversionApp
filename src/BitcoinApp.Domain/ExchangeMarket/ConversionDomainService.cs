namespace BitcoinApp.Domain.ExchangeMarket;

public class ConversionDomainService(IExchangeMarketRepository exchangeMarketRepository)
{
    private readonly IExchangeMarketRepository _exchangeMarketRepository = exchangeMarketRepository;
}
