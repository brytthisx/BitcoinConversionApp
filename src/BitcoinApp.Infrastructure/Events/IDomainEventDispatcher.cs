using BitcoinApp.Domain;

namespace BitcoinApp.Infrastructure.Events;

public interface IDomainEventDispatcher
{
    Task Dispatch(IDomainEvent domainEvent);
}
