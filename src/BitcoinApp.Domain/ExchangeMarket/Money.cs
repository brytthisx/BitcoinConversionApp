namespace BitcoinApp.Domain.ExchangeMarket;

public sealed record Money(decimal Amount, string Currency = "CZK");
