namespace BitcoinApp.Domain.ExchangeMarket;

public class ConversionDomainService(IConversionRepository conversionRepository)
{
    private readonly IConversionRepository _conversionRepository = conversionRepository;
}
