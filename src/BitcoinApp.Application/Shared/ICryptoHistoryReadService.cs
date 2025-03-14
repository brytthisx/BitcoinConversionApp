using BitcoinApp.Application.CryptoHistory.GetCryptoHistory;

namespace BitcoinApp.Application.Shared;

public interface ICryptoHistoryReadService
{
    IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken)
        where T : class;

    Task<GetCryptoHistoryRecordsDto> GetCryptoHistoryRecords(CancellationToken cancellationToken);
}
