using BitcoinApp.Domain;
using MassTransit.Mediator;

namespace BitcoinApp.Infrastructure.Events;

public class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public Task Dispatch(IDomainEvent domainEvent)
    {
        mediator.Publish(domainEvent);
        return Task.CompletedTask;
    }
}
