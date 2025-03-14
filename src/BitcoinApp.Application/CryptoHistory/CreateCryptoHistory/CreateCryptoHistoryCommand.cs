using BitcoinApp.Domain.CryptoHistory;

namespace BitcoinApp.Application.CryptoHistory;

public sealed record CreateCryptoHistoryCommand(DateTime HistoryDate, Money defaultCurrency, Money convertedCurrency, string Comment);
