using BitcoinApp.Application.Crypto.GetLatestConversion;

namespace BitcoinApp.Application.Shared;

public interface IConversionReadService
{
    IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken)
        where T : class;

    Task<GetLatestConversionDto> GetLatestConversion(CancellationToken cancellationToken);
}
