using BitcoinApp.Domain.ExchangeMarket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitcoinApp.Infrastructure.Repositories;

public class ConversionConfiguration : IEntityTypeConfiguration<Conversion>
{
    public void Configure(EntityTypeBuilder<Conversion> builder)
    {
        builder.HasKey(x => x.ConversionId);
        builder.Property(x => x.ConversionId).HasConversion(x => x.Value, v => new ConversionId(v));
        builder.OwnsOne(x => x.ActualConversion, price =>
        {
            price.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
            price.Property(x => x.Currency).HasMaxLength(3).IsRequired();
        });

        builder.Property(x => x.ConversionDate).IsRequired();
    }
}
