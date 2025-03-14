using BitcoinApp.Domain.CryptoHistory;
using MassTransit;

namespace BitcoinApp.Application.CryptoHistory.UpdateCryptoHistory;

public class UpdateCryptoHistoryCommandHandler(ICryptoHistoryRepository historyRepository)
    : IConsumer<UpdateCryptoHistoryCommand>
{
    public async Task Consume(ConsumeContext<UpdateCryptoHistoryCommand> context)
    {
        CryptoHistoryRecord cryptoHistoryRecord =
            await historyRepository.GetCryptoHistoryRecord(context.Message.historyId);

        if (cryptoHistoryRecord == null)
        {
            throw new KeyNotFoundException(nameof(CryptoHistoryRecord));
        }

    }
}
