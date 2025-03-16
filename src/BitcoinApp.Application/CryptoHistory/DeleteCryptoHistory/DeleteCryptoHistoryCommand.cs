using BitcoinApp.Domain.CryptoHistory;
using MediatR;

namespace BitcoinApp.Application.CryptoHistory.DeleteCryptoHistory;

public sealed record DeleteCryptoHistoryCommand(HistoryId HistoryId) : IRequest<DeleteCryptoHistoryCommandResponse>;
