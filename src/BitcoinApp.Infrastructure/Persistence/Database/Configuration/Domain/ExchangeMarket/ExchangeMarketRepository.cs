using BitcoinApp.Domain.ExchangeMarket;
using BitcoinApp.Infrastructure.Exceptions;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.Repositories;

public class ExchangeMarketRepository(AppDbContext appDbContext) : IExchangeMarketRepository
{
    public async Task AddAsync(Conversion conversion, CancellationToken cancellationToken = default)
    {
        await appDbContext.AddAsync(conversion, cancellationToken);
    }

    public async Task<Conversion> GetLatestConversion(CancellationToken cancellationToken = default)
    {
        Conversion record = await appDbContext.Conversions.FirstAsync(cancellationToken);

        if (record is null)
        {
            throw new ItemsNotFoundException();
        }

        return record;
    }
}
