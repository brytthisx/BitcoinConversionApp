using BitcoinApp.Domain.ExchangeMarket;

namespace BitcoinApp.Application.Crypto.GetLatestConversion;

public sealed record GetLatestConversionDto(Money originalCurrency, Money convertedCurrency, DateTime conversionDate);
