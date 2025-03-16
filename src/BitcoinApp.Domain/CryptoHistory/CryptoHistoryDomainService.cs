namespace BitcoinApp.Domain.CryptoHistory;

public class CryptoHistoryDomainService(ICryptoHistoryRecordRepository cryptoHistoryRepository)
{
    private readonly ICryptoHistoryRecordRepository _cryptoHistoryRepository = cryptoHistoryRepository;
}
