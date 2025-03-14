using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Infrastructure.Exceptions;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.Persistence.Database.Configuration.Domain.HistoryRecords;

public class CryptoHistoryRecordRepository(AppDbContext appDbContext) : ICryptoHistoryRepository
{
    public async Task AddAsync(CryptoHistoryRecord cryptoHistoryRecord, CancellationToken cancellationToken = default)
    {
        await appDbContext.AddAsync(cryptoHistoryRecord, cancellationToken);
    }

    public async Task UpdateAsync(HistoryId historyId, string comment, CancellationToken cancellationToken = default)
    {

        CryptoHistoryRecord? record = await appDbContext.CryptoHistoryRecords
            .FirstOrDefaultAsync(r => r.HistoryId == historyId, cancellationToken);

        if (record == null)
        {
            throw new EntityNotFoundException($"CryptoHistoryRecord with HistoryId {historyId} not found.");
        }

        appDbContext.Update(record);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HistoryId historyId, CancellationToken cancellationToken = default)
    {
        CryptoHistoryRecord? record = await appDbContext.CryptoHistoryRecords
            .FirstOrDefaultAsync(r => r.HistoryId == historyId, cancellationToken);

        if (record == null)
        {
            throw new EntityNotFoundException($"CryptoHistoryRecord with HistoryId {historyId} not found.");
        }

        appDbContext.CryptoHistoryRecords.Remove(record);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CryptoHistoryRecord>> GetCryptoHistoryRecords(CancellationToken cancellationToken = default)
    {
        return await appDbContext.CryptoHistoryRecords.ToListAsync(cancellationToken);
    }

    public async Task<CryptoHistoryRecord> GetCryptoHistoryRecord(HistoryId historyId,
        CancellationToken cancellationToken = default)
    {
        CryptoHistoryRecord? record = await appDbContext.CryptoHistoryRecords
            .FirstOrDefaultAsync(r => r.HistoryId == historyId, cancellationToken);

        if (record == null)
        {
            throw new EntityNotFoundException($"CryptoHistoryRecord with HistoryId {historyId} not found.");
        }

        return record;
    }
}
