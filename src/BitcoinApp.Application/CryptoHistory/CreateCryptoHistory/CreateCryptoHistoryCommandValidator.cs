using FluentValidation;

namespace BitcoinApp.Application.CryptoHistory;

public class CreateCryptoHistoryCommandValidator : AbstractValidator<CreateCryptoHistoryCommand>
{
    public CreateCryptoHistoryCommandValidator()
    {
        RuleFor(x => x.defaultCurrency).NotEmpty();
    }
}
