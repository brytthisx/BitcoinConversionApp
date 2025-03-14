namespace BitcoinApp.Domain.CryptoHistory.DomainEvents;

public sealed record CryptoHistoryCreatedDomainEvent(Guid HistoryId) : IDomainEvent;
