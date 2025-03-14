using BitcoinApp.Application.Shared;
using BitcoinApp.Domain;

namespace BitcoinApp.Infrastructure.Events;

public interface IEventMapper
{
    IntegrationEvent Map(IDomainEvent domainEvent);
}
