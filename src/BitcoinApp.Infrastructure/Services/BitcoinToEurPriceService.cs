using BitcoinApp.Application.Shared;
using BitcoinApp.Domain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BitcoinApp.Infrastructure.Services;

public class BitcoinToEurPriceService(HttpClient httpClient, IConfiguration configuration) : IBitcoinToEurPriceService
{
    private readonly string _apiUrl = configuration["BitcoinApi:Url"]
                                      ?? throw new InvalidOperationException("BitcoinApi:Url configuration is missing.");

    public async Task<(decimal Price, string TargetCurrency, DateTime DateOfRecord)> GetLatestBtcEurPriceAsync()
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(new Uri(_apiUrl));
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            CryptoApiResponseDto? bitcoinPriceDto = JsonConvert.DeserializeObject<CryptoApiResponseDto>(responseBody);
            CryptoApiResponseDto.CoinBaseResponseDto? priceData = bitcoinPriceDto?.Data;
            if (priceData == null)
            {
                throw new InvalidOperationException("Price data not found in the response.");
            }

            CryptoApiResponseDto.CoinBaseResponseDto.BtcEur etcEurData = priceData.BtcEurData;

            DateTime dateOfRecord = DateTimeOffset.FromUnixTimeSeconds(etcEurData.PriceLastUpdateTs).DateTime;

            return (etcEurData.Price, etcEurData.Quote, dateOfRecord);
        }
        catch (Exception ex)
        {
            throw new HttpRequestException("Error fetching Bitcoin price", ex);
        }
    }
}
