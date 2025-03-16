using MediatR;
using BitcoinApp.Domain.CryptoHistory;
using BitcoinApp.Domain;
using BitcoinApp.Domain.CryptoHistory.DomainEvents;

namespace BitcoinApp.Application.CryptoHistory.DeleteCryptoHistory;

public class DeleteCryptoHistoryCommandHandler(ICryptoHistoryRecordRepository historyRepository)
    : IRequestHandler<DeleteCryptoHistoryCommand, DeleteCryptoHistoryCommandResponse>
{
    public async Task<DeleteCryptoHistoryCommandResponse> Handle(DeleteCryptoHistoryCommand request, CancellationToken cancellationToken)
    {
        var record = await historyRepository.GetByIdAsync(request.HistoryId, cancellationToken);

        if (record == null)
        {
            return null; // Return null to indicate not found
        }

        // Mark entity as deleted instead of trying to remove it
        historyRepository.Remove(record);

        // Persist changes
        await historyRepository.SaveChangesAsync(cancellationToken);

        // Publish an event for the deletion (optional)
        // You can use MediatR.Publish() for domain events if needed

        return new DeleteCryptoHistoryCommandResponse(record.HistoryId.Value)
        ;
    }
}
