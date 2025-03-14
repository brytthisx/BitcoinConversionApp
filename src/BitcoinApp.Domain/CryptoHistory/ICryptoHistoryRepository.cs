namespace BitcoinApp.Domain.CryptoHistory;

public interface ICryptoHistoryRepository
{
    Task AddAsync(CryptoHistoryRecord cryptoHistoryRecord, CancellationToken cancellationToken = default);

    Task UpdateAsync(HistoryId historyId, string comment, CancellationToken cancellationToken = default);

    Task DeleteAsync(HistoryId historyId, CancellationToken cancellationToken = default);
    
    Task<List<CryptoHistoryRecord>> GetCryptoHistoryRecords(CancellationToken cancellationToken = default);

    Task<CryptoHistoryRecord>
        GetCryptoHistoryRecord(HistoryId historyId, CancellationToken cancellationToken = default);
}
