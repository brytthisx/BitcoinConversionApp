using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Domain.ExchangeMarket;
using Microsoft.EntityFrameworkCore;

namespace BitcoinApp.Infrastructure.Persistence.Database.MsSql;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<CryptoHistoryRecord> CryptoHistoryRecords { get; set; }
    public DbSet<Conversion> Conversions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        // Add the DomainEvent entity to the model
        modelBuilder.Entity<DomainEvent>(entity =>
        {
            entity.HasKey(e => e.DomainEventId);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.AssemblyName).IsRequired();
            entity.Property(e => e.OccuredAt).IsRequired();
            entity.Property(e => e.Payload).IsRequired();
        });
    }
}
