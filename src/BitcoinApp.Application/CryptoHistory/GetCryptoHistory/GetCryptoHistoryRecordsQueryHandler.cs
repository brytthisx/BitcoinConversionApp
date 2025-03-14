using MassTransit;
using BitcoinApp.Application.Shared;

namespace BitcoinApp.Application.CryptoHistory.GetCryptoHistory;

public class GetCryptoHistoryRecordsQueryHandler(ICryptoHistoryReadService cryptoHistoryReadService)
    : IConsumer<GetCryptoHistoryRecordsQuery>
{
    public async Task Consume(ConsumeContext<GetCryptoHistoryRecordsQuery> context)
    {
        GetCryptoHistoryRecordsDto? records =
            await cryptoHistoryReadService.GetCryptoHistoryRecords(context.CancellationToken);

        await context.RespondAsync(records);
    }
}
