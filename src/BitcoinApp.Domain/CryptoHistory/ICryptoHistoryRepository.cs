namespace BitcoinApp.Domain.CryptoHistory;

public interface ICryptoHistoryRecordRepository
{
    Task AddAsync(CryptoHistoryRecord cryptoHistoryRecord, CancellationToken cancellationToken = default);

    Task<CryptoHistoryRecord?> GetByIdAsync(HistoryId historyId, CancellationToken cancellationToken = default);

    Task<List<CryptoHistoryRecord>> GetAllAsync(CancellationToken cancellationToken = default);


    Task Update(CryptoHistoryRecord cryptoHistoryRecord, CancellationToken cancellationToken = default);
    void Remove(CryptoHistoryRecord record);
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
