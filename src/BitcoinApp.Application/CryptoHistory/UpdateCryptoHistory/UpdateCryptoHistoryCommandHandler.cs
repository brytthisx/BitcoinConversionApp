using MediatR;
using BitcoinApp.Domain.CryptoHistory;

namespace BitcoinApp.Application.CryptoHistory.UpdateCryptoHistory;

public class UpdateCryptoHistoryCommandHandler(ICryptoHistoryRecordRepository historyRepository)
    : IRequestHandler<UpdateCryptoHistoryCommand, UpdateCryptoHistoryCommandResponse>
{
    public async Task<UpdateCryptoHistoryCommandResponse?> Handle(UpdateCryptoHistoryCommand request, CancellationToken cancellationToken)
    {
        var record = await historyRepository.GetByIdAsync(request.HistoryId, cancellationToken);

        if (record == null)
        {
            return null; // Return null if the record does not exist
        }

        // Update only if new data is provided
        if (!string.IsNullOrEmpty(request.Data.Comment))
        {
            record.UpdateComment(request.Data.Comment);
        }

        await historyRepository.SaveChangesAsync(cancellationToken); // Save changes

        return new UpdateCryptoHistoryCommandResponse(record.HistoryId.Value); // Return updated HistoryId
    }
}
