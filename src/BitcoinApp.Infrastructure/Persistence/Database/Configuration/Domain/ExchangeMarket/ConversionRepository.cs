using BitcoinApp.Domain.ExchangeMarket;
using BitcoinApp.Infrastructure.Exceptions;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.Persistence.Database.Configuration.Domain;

public class ConversionRepository(AppDbContext appDbContext) : IConversionRepository
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
