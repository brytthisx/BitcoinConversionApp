using System.Globalization;
using BitcoinApp.Domain.ExchangeMarket;
using BitcoinApp.Domain.Services;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BitcoinApp.Infrastructure.Services;

public class ExchangeConversionService(HttpClient httpClient, IConfiguration configuration, AppDbContext context)
    : IExchangeConversionService
{
    private readonly string _apiUrl = configuration["DividendMarketApi:Url"]
                                      ?? throw new InvalidOperationException("DividendMarketApi:Url configuration is missing.");

    public async Task<decimal> GetLatestConversionRateToCZKAsync(string targetCurrency)
    {
        HttpResponseMessage response = await httpClient.GetAsync(new Uri(_apiUrl));
        response.EnsureSuccessStatusCode();

        string responseData = await response.Content.ReadAsStringAsync();
        IEnumerable<string> lines = responseData.Split('\n')
            .Skip(2) // Skip the first two lines (header)
            .Where(line => !string.IsNullOrWhiteSpace(line)); // Remove empty lines

        foreach (string line in lines)
        {
            string[] columns = line.Split('|');
            if (columns[3].Trim() == targetCurrency &&
                decimal.TryParse(columns[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rate))
            {
                return rate;
            }
        }

        throw new Exception("Failed to parse exchange rate from response.");
    }

    public async Task<decimal> GetExchangeConversionRateAsync(string fromCurrency, string toCurrency)
    {
        if (fromCurrency != "EUR" || toCurrency != "CZK")
        {
            throw new Exception("Invalid currency pair (not implemented)");
        }

        var exchangeRate = await context.Conversions
            .Where(rate => rate.ActualConversion.Currency == toCurrency)
            .Select(conversion => new { conversion.ConversionDate, conversion.ActualConversion.Amount })
            .FirstOrDefaultAsync();


        DateTime currentTime = DateTime.UtcNow;
        DateTime today = DateTime.UtcNow.Date;
        DateTime cutoffTime = today.AddHours(12).AddMinutes(30); // 14:30 European time (UTC+2)

        if (exchangeRate == null || (currentTime > cutoffTime && exchangeRate.ConversionDate < cutoffTime))
        {
            decimal rate = await GetLatestConversionRateToCZKAsync(fromCurrency);

            // save the new rate to the database
            context.Conversions.Add(Conversion.Create
            (currentTime,
                new Money(rate, toCurrency)
            ));
            await context.SaveChangesAsync();
            return rate;
        }
        else
        {
            return exchangeRate.Amount;
        }
    }
}
