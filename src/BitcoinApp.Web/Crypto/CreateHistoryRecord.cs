namespace BitcoinApp.Web.Crypto;

public class CreateHistoryRecord
{
    public DateTime HistoryDate { get; set; }
    public Price DefaultCurrency { get; set; }

    public Price ConvertedCurrency { get; set; }
    public string Comment { get; set; }

}

