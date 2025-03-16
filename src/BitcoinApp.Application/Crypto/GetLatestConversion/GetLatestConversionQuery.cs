using MediatR;

namespace BitcoinApp.Application.Crypto.GetLatestConversion;

public sealed record GetLatestConversionQuery() : IRequest<GetLatestConversionDto?>;
