namespace BitcoinApp.Web.Crypto;


public class LatestConversion
{
    public DateTime conversionDate { get; set; }
    public required Price originalCurrency { get; set; }
    public required Price convertedCurrency { get; set; }
}
