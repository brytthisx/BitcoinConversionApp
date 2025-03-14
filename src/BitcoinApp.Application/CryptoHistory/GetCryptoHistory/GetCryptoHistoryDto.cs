using BitcoinApp.Domain.CryptoHistory;

namespace BitcoinApp.Application.CryptoHistory.GetCryptoHistory;

public sealed record GetCryptoHistoryRecordsDto(List<GetCryptoHistoryRecordDto> Records, int TotalRecords);

public sealed record GetCryptoHistoryRecordDto(CryptoHistoryRecord ResponseObject);
