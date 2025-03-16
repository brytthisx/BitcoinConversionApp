using BitcoinApp.Domain.CryptoHistory;
using MediatR;

namespace BitcoinApp.Application.CryptoHistory.CreateCryptoHistory;

public sealed record CreateCryptoHistoryCommand(DateTime HistoryDate, Money DefaultCurrency, Money ConvertedCurrency, string Comment) : IRequest<CreateCryptoHistoryCommandResponse>;
