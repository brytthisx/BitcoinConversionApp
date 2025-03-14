using BitcoinApp.Domain.CryptoHistory;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.Persistence.Database.MsSql;

public interface IAppDbContext
{
    public DbSet<CryptoHistoryRecord> CryptoHistoryRecords { get; set; }
}
