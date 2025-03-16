using BitcoinApp.Web.Crypto;

namespace BitcoinApp.Web;

public class CryptoApiClient(HttpClient httpClient)
{

    public async Task<LatestConversion> GetLatestConversionAsync(CancellationToken cancellationToken = default)
    {
        var latestConversion = await httpClient.GetFromJsonAsync<LatestConversion>("/api/crypto", cancellationToken);

        return latestConversion ?? throw new InvalidOperationException("Failed to retrieve latest conversion.");
    }

    public async Task<HistoryRecordData> GetCryptoHistoryRecordsAsync(CancellationToken cancellationToken = default)
    {
        var historyRecords = await httpClient.GetFromJsonAsync<HistoryRecordData>("/api/CryptoHistory", cancellationToken);

        Console.WriteLine("History records: " + historyRecords);
        if (historyRecords == null)
        {
            throw new InvalidOperationException("Failed to retrieve crypto history records.");
        }

        return historyRecords;
    }

    public async Task CreateHistoryRecordAsync(CreateHistoryRecord record, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/CryptoHistory", record, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task SaveRecordCommentAsync(HistoryId historyId, string comment, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"/api/CryptoHistory/{historyId.Value}", new { comment }, cancellationToken);

        response.EnsureSuccessStatusCode();


    }

    public async Task DeleteHistoryRecordAsync(HistoryId historyId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/api/CryptoHistory/{historyId.Value}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
