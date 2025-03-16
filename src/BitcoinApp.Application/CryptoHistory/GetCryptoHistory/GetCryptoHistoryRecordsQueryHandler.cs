using MediatR;
using BitcoinApp.Application.Shared;

namespace BitcoinApp.Application.CryptoHistory.GetCryptoHistory;

public class GetCryptoHistoryRecordsQueryHandler(ICryptoHistoryReadService cryptoHistoryReadService)
    : IRequestHandler<GetCryptoHistoryRecordsQuery, GetCryptoHistoryRecordsDto>
{
    public async Task<GetCryptoHistoryRecordsDto> Handle(GetCryptoHistoryRecordsQuery request, CancellationToken cancellationToken)
    {
        return await cryptoHistoryReadService.GetCryptoHistoryRecords(cancellationToken);
    }
}
