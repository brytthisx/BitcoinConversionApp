using FluentValidation;

namespace BitcoinApp.Application.CryptoHistory.CreateCryptoHistory;

public class CreateCryptoHistoryCommandValidator : AbstractValidator<CreateCryptoHistoryCommand>
{
    public CreateCryptoHistoryCommandValidator()
    {
        RuleFor(x => x.DefaultCurrency).NotEmpty();
    }
}
