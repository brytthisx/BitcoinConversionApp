namespace BitcoinApp.Web.Crypto;

public class HistoryId
{
    public Guid Value { get; set; }
}

public class ResponseObject
{
    public HistoryId HistoryId { get; set; }
    public DateTime HistoryDate { get; set; }
    public Price OriginalPrice { get; set; }
    public Price ConvertedPrice { get; set; }
    public string Comment { get; set; }
    public List<DomainEvent> DomainEvents { get; set; }
}

public class DomainEvent
{
}

public class Record
{
    public ResponseObject ResponseObject { get; set; }
}

public class HistoryRecordData
{
    public List<Record> Records { get; set; }
    public int TotalRecords { get; set; }
}
