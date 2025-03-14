namespace BitcoinApp.Domain.ExchangeMarket.DomainEvents;

public sealed record ConversionCreatedDomainEvent(Guid ConversionId) : IDomainEvent;
