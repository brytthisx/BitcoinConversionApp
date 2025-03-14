using BitcoinApp.Application.CryptoHistory.GetCryptoHistory;
using BitcoinApp.Application.Shared;
using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.ReadServices;

public class CryptoHistoryReadService(AppDbContext dbContext) : ICryptoHistoryReadService
{
    public IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken)
        where T : class
    {
        return dbContext.Set<T>()
            .FromSqlRaw(sql, parameters)
            .AsNoTracking();
    }

    public async Task<GetCryptoHistoryRecordsDto> GetCryptoHistoryRecords(CancellationToken cancellationToken)
    {
        IQueryable<CryptoHistoryRecord> query = dbContext.CryptoHistoryRecords.AsQueryable();

        List<CryptoHistoryRecord> cryptoHistoryRecords = await query
            .AsNoTracking()
            .TagWithCallSite()
            .ToListAsync(cancellationToken);

        List<GetCryptoHistoryRecordDto> cryptoHistoryRecordDtos = cryptoHistoryRecords
            .Select(record => new GetCryptoHistoryRecordDto(
                record))
            .ToList();

        return new GetCryptoHistoryRecordsDto
        (
            cryptoHistoryRecordDtos, cryptoHistoryRecordDtos.Count
        );
    }
}
