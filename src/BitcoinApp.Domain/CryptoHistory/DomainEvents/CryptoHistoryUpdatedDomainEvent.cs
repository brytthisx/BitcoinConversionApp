namespace BitcoinApp.Domain.CryptoHistory.DomainEvents;

public record CryptoHistoryUpdatedDomainEvent(Guid HistoryId, string Comment) : IDomainEvent;
