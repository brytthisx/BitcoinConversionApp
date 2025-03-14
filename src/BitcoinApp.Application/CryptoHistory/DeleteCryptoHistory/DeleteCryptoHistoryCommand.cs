using BitcoinApp.Domain.CryptoHistory;

namespace BitcoinApp.Application.CryptoHistory.DeleteCryptoHistory;

public sealed record DeleteCryptoHistoryCommand(HistoryId historyId);
