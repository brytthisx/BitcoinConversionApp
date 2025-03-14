using Newtonsoft.Json;

namespace BitcoinApp.Application.Shared;

public class CryptoApiResponseDto
{
    [JsonProperty(nameof(Data))] public required CoinBaseResponseDto Data { get; set; }

    public class CoinBaseResponseDto
    {
        [JsonProperty("BTC-EUR")] public required BtcEur BtcEurData { get; set; }

        public class BtcEur
        {
            [JsonProperty("QUOTE")] public required string Quote { get; set; }

            [JsonProperty("PRICE_LAST_UPDATE_TS")] public required long PriceLastUpdateTs { get; set; }

            [JsonProperty("PRICE")] public required decimal Price { get; set; }
        }
    }
}
