namespace BitcoinApp.Domain.ExchangeMarket;

public sealed record ConversionId(Guid Value)
{
    public static explicit operator Guid(ConversionId conversionId)
    {
        return conversionId.Value;
    }
}
