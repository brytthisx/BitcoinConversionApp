using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.Persistence;

internal sealed class DbInitializer(
    AppDbContext context) : IDbInitializer
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            Console.WriteLine("Applied database migrations for CryptoHistoryRecord module");
        }
    }

    public Task SeedAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
