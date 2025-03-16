namespace BitcoinApp.Domain.CryptoHistory.DomainEvents;

public record CryptoHistoryDeletedDomainEvent(Guid HistoryId) : IDomainEvent;
