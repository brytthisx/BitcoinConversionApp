using MediatR;
using BitcoinApp.Domain;

namespace BitcoinApp.Infrastructure.Events;

public class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task Dispatch(IDomainEvent domainEvent)
    {
        await mediator.Publish(domainEvent);
    }
}
