using BitcoinApp.Domain.CryptoHistory.DomainEvents;

namespace BitcoinApp.Domain.CryptoHistory;

public class CryptoHistoryRecord : Entity
{
    public HistoryId HistoryId { get; private set; }
    public DateTime HistoryDate { get; private set; }
    public Money OriginalPrice { get; private set; }
    public Money ConvertedPrice { get; private set; }
    public string Comment { get; private set; }
    public bool IsDeleted { get; private set; } // Soft delete flag

    private CryptoHistoryRecord() { } 

    public static CryptoHistoryRecord Create(DateTime historyDate, Money originalPrice, Money convertedPrice, string comment)
    {
        var record = new CryptoHistoryRecord(historyDate, originalPrice, convertedPrice, comment);
        record.AddDomainEvent(new CryptoHistoryCreatedDomainEvent(record.HistoryId.Value));
        return record;
    }

public void UpdateComment(string newComment)
{
    Comment = newComment;
    AddDomainEvent(new CryptoHistoryUpdatedDomainEvent(HistoryId.Value, newComment));
}

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        AddDomainEvent(new CryptoHistoryDeletedDomainEvent(HistoryId.Value));
    }

    private CryptoHistoryRecord(DateTime historyDate, Money originalPrice, Money convertedPrice, string comment)
    {
        HistoryId = new HistoryId(Guid.NewGuid());
        HistoryDate = historyDate;
        OriginalPrice = originalPrice;
        ConvertedPrice = convertedPrice;
        Comment = comment;
    }
}
