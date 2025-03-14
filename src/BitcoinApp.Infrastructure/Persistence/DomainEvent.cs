namespace BitcoinApp.Infrastructure.Persistence;

public sealed record DomainEvent(
    Guid DomainEventId,
    DateTime OccuredAt,
    string Type,
    string AssemblyName,
    string Payload)
{
    public DateTime? CompletedAt { get; set; }
}
