using BitcoinApp.Domain.CryptoHistory;

namespace BitcoinApp.Application.CryptoHistory.UpdateCryptoHistory;

public sealed record UpdateCryptoHistoryCommand(HistoryId historyId, string comment);
