using MassTransit;
using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Domain;

namespace BitcoinApp.Application.CryptoHistory.CreateCryptoHistory
{
    public class CreateCryptoHistoryCommandHandler : IConsumer<CreateCryptoHistoryCommand>
    {
        private readonly ICryptoHistoryRepository _historyRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CryptoHistoryDomainService _historyDomainService;

        public CreateCryptoHistoryCommandHandler(
            ICryptoHistoryRepository historyRepository,
            IDateTimeProvider dateTimeProvider,
            CryptoHistoryDomainService historyDomainService)
        {
            _historyRepository = historyRepository;
            _dateTimeProvider = dateTimeProvider;
            _historyDomainService = historyDomainService;
        }

        public async Task Consume(ConsumeContext<CreateCryptoHistoryCommand> context)
        {
            var record = CryptoHistoryRecord.Create(
                context.Message.HistoryDate,
                context.Message.defaultCurrency,
                context.Message.convertedCurrency,
                context.Message.Comment);

            await _historyRepository.AddAsync(record, context.CancellationToken);
            await context.RespondAsync(new CreateCryptoHistoryCommandResponse(record.HistoryId));
        }
    }
}
