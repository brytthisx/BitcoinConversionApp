using BitcoinApp.Domain.ExchangeMarket.DomainEvents;

namespace BitcoinApp.Domain.ExchangeMarket;

public class Conversion : Entity
{
    public ConversionId ConversionId { get; private set; }
    public DateTime ConversionDate { get; private set; }
    public Money ActualConversion { get; private set; }

    private Conversion() { }

    public static Conversion Create(DateTime conversionDate, Money actualConversion)
    {
        return new Conversion(
            conversionDate,
            actualConversion
        );
    }

    private Conversion(DateTime conversionDate, Money actualConversion)
    {
        ConversionId = new ConversionId(Guid.NewGuid());
        ConversionDate = conversionDate;
        ActualConversion = actualConversion;

        AddDomainEvent(new ConversionCreatedDomainEvent(ConversionId.Value));
    }
}
