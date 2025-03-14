using BitcoinApp.Domain.CryptoHistory.DomainEvents;

namespace BitcoinApp.Domain.CryptoHistory;

public class CryptoHistoryRecord : Entity
{
    public HistoryId HistoryId { get; private set; }
    public DateTime HistoryDate { get; private set; }
    public Money OriginalPrice { get; private set; }
    public Money ConvertedPrice { get; private set; }
    public string Comment { get; private set; }

    private CryptoHistoryRecord() { }

    public static CryptoHistoryRecord Create(DateTime historyDate, Money originalPrice, Money convertedPrice, string comment)
    {
        return new CryptoHistoryRecord(
            historyDate,
            originalPrice, 
            convertedPrice,
            comment
        );
    }

    public static CryptoHistoryRecord Update(HistoryId historyId, string comment)
    {
        return new CryptoHistoryRecord
        {
            HistoryId = historyId,
            Comment = comment
        };
    }

    public static CryptoHistoryRecord Delete(HistoryId historyId)
    {
        return new CryptoHistoryRecord
        {
            HistoryId = historyId
        };
    }

    private CryptoHistoryRecord(DateTime historyDate, Money originalPrice, Money convertedPrice, string comment)
    {
        HistoryId = new HistoryId(Guid.NewGuid());
        HistoryDate = historyDate;
        OriginalPrice = originalPrice;
        ConvertedPrice = convertedPrice;
        Comment = comment;

        AddDomainEvent(new CryptoHistoryCreatedDomainEvent(HistoryId.Value));
    }
}
