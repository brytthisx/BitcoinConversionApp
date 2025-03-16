using MediatR;

namespace BitcoinApp.Application.CryptoHistory.GetCryptoHistory;

public sealed record GetCryptoHistoryRecordsQuery() : IRequest<GetCryptoHistoryRecordsDto>;

public sealed record GetCryptoHistoryRecordQuery(Guid RecordId) : IRequest<GetCryptoHistoryRecordDto>;
