using BitcoinApp.Application.Shared;
using MassTransit;

namespace BitcoinApp.Application.Crypto.GetLatestConversion;

public class GetLatestConversionQueryHandler(IConversionReadService conversionReadService)
    : IConsumer<GetLatestConversionQuery>
{
    public async Task Consume(ConsumeContext<GetLatestConversionQuery> context)
    {
        GetLatestConversionDto? record = await conversionReadService.GetLatestConversion(context.CancellationToken);
        await context.RespondAsync(record);
    }
}
