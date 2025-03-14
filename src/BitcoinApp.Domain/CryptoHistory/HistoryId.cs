namespace BitcoinApp.Domain.CryptoHistory;

public sealed record HistoryId(Guid Value)
{
    public static explicit operator Guid(HistoryId historyId)
    {
        return historyId.Value;
    }
}
