using BitcoinApp.Domain.CryptoHistory;
using MediatR;

namespace BitcoinApp.Application.CryptoHistory.UpdateCryptoHistory;

public sealed record UpdateCryptoHistoryCommand(HistoryId HistoryId, UpdateCryptoHistoryDto Data) : IRequest<UpdateCryptoHistoryCommandResponse>;
