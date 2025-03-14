using MassTransit;
using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Domain;

namespace BitcoinApp.Application.CryptoHistory.DeleteCryptoHistory
{
    public class DeleteCryptoHistoryCommandHandler : IConsumer<DeleteCryptoHistoryCommand>
    {
        private readonly ICryptoHistoryRepository _historyRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CryptoHistoryDomainService _historyDomainService;

        public DeleteCryptoHistoryCommandHandler(
            ICryptoHistoryRepository historyRepository,
            IDateTimeProvider dateTimeProvider,
            CryptoHistoryDomainService historyDomainService)
        {
            _historyRepository = historyRepository;
            _dateTimeProvider = dateTimeProvider;
            _historyDomainService = historyDomainService;
        }

        public async Task Consume(ConsumeContext<DeleteCryptoHistoryCommand> context)
        {
            var record = CryptoHistoryRecord.Delete(
                context.Message.historyId);

            await _historyRepository.DeleteAsync(record.HistoryId, context.CancellationToken);
            await context.RespondAsync(new DeleteCryptoHistoryCommandResponse());
        }
    }
}
