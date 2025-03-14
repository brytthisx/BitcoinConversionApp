namespace BitcoinApp.Domain.CryptoHistory;

public class CryptoHistoryDomainService(ICryptoHistoryRepository cryptoHistoryRepository)
{
    private readonly ICryptoHistoryRepository _cryptoHistoryRepository = cryptoHistoryRepository;
}
