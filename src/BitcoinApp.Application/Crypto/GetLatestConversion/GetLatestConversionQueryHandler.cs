using MediatR;
using BitcoinApp.Application.Shared;

namespace BitcoinApp.Application.Crypto.GetLatestConversion;

public class GetLatestConversionQueryHandler(IConversionReadService conversionReadService)
    : IRequestHandler<GetLatestConversionQuery, GetLatestConversionDto?>
{
    public async Task<GetLatestConversionDto?> Handle(GetLatestConversionQuery request, CancellationToken cancellationToken)
    {
        return await conversionReadService.GetLatestConversion(cancellationToken);
    }
}
