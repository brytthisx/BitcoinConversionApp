using BitcoinApp.Domain.CryptoHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitcoinApp.Infrastructure.Repositories;

public class CryptoHistoryRecordsConfiguration : IEntityTypeConfiguration<CryptoHistoryRecord>
{
    public void Configure(EntityTypeBuilder<CryptoHistoryRecord> builder)
    {
        builder.HasKey(x => x.HistoryId);
        builder.Property(x => x.HistoryId).HasConversion(x => x.Value, v => new HistoryId(v));
        builder.OwnsOne(x => x.OriginalPrice, price =>
        {
            price.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
            price.Property(x => x.Currency).HasMaxLength(3).IsRequired();
        });
        builder.OwnsOne(x => x.ConvertedPrice, price =>
        {
            price.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
            price.Property(x => x.Currency).HasMaxLength(3).IsRequired();
        });
        builder.Property(x => x.Comment).HasMaxLength(100);

        builder.Property(x => x.HistoryDate).IsRequired();
    }
}
