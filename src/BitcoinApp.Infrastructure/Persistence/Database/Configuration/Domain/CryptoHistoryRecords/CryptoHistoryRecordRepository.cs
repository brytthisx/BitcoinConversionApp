using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.Repositories;

public class CryptoHistoryRecordRepository(AppDbContext dbContext) : ICryptoHistoryRecordRepository
{
    public async Task AddAsync(CryptoHistoryRecord cryptoHistoryRecord, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<CryptoHistoryRecord>().AddAsync(cryptoHistoryRecord, cancellationToken);
    }

    public async Task<CryptoHistoryRecord?> GetByIdAsync(HistoryId historyId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<CryptoHistoryRecord>()
            .FirstOrDefaultAsync(x => x.HistoryId == historyId, cancellationToken);
    }

    public async Task<List<CryptoHistoryRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<CryptoHistoryRecord>()
            .Where(x => !x.IsDeleted) // Exclude soft-deleted records
            .ToListAsync(cancellationToken);
    }

    public void Remove(CryptoHistoryRecord record)
    {
        dbContext.CryptoHistoryRecords.Remove(record);
    }

    public Task Update(CryptoHistoryRecord cryptoHistoryRecord, CancellationToken cancellationToken = default)
    {
        dbContext.Set<CryptoHistoryRecord>().Update(cryptoHistoryRecord);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
