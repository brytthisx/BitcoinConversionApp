using MediatR;
using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Domain;

namespace BitcoinApp.Application.CryptoHistory.CreateCryptoHistory;

public class CreateCryptoHistoryCommandHandler : IRequestHandler<CreateCryptoHistoryCommand, CreateCryptoHistoryCommandResponse>
{
    private readonly ICryptoHistoryRecordRepository _historyRepository;

    public CreateCryptoHistoryCommandHandler(
        ICryptoHistoryRecordRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task<CreateCryptoHistoryCommandResponse> Handle(CreateCryptoHistoryCommand request, CancellationToken cancellationToken)
    {
        var record = CryptoHistoryRecord.Create(
            request.HistoryDate,
            new Money(request.DefaultCurrency.Amount, request.DefaultCurrency.Currency),
            new Money(request.ConvertedCurrency.Amount, request.ConvertedCurrency.Currency),
            request.Comment
        );

        await _historyRepository.AddAsync(record, cancellationToken);
        await _historyRepository.SaveChangesAsync(cancellationToken);

        return new CreateCryptoHistoryCommandResponse(record.HistoryId);
    }
}
