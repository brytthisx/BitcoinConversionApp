using BitcoinApp.Application.Shared;
using BitcoinApp.Domain;
using BitcoinApp.Infrastructure.Events;
using BitcoinApp.Infrastructure.Persistence;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using MassTransit;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace BitcoinApp.Infrastructure.MassTransit;

public class EventsFilter<T>(
    AppDbContext appDbContext,
    IDateTimeProvider dateTimeProvider,
    EventMapperFactory mapperFactory)
    : IFilter<ConsumeContext<T>>
    where T : class
{
    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        await next.Send(context);

        IEnumerable<EntityEntry<Entity>> entities = appDbContext.ChangeTracker.Entries<Entity>()
            .Where(e => e.Entity.DomainEvents is not null && e.Entity.DomainEvents.Any());

        List<IDomainEvent> events = entities.SelectMany(x => x.Entity.DomainEvents).ToList();
        entities.ToList().ForEach(x => x.Entity.ClearDomainEvents());

        foreach (IDomainEvent domainEvent in events)
        {
            await appDbContext.AddAsync<DomainEvent>(
                new DomainEvent(
                    Guid.NewGuid(),
                    dateTimeProvider.UtcNow,
                    domainEvent.GetType().FullName,
                    domainEvent.GetType().Assembly.GetName().Name,
                    JsonConvert.SerializeObject(domainEvent,
                        new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All })));
        }

        await appDbContext.AddRangeAsync(RemapToIntegrationEvents(events));

        await appDbContext.SaveChangesAsync();
    }

    public void Probe(ProbeContext context) { }

    public List<IntegrationEvent> RemapToIntegrationEvents(List<IDomainEvent> domainEvents)
    {
        List<IntegrationEvent> integrationEvents = new();
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            IntegrationEvent? intergrationEvent = mapperFactory
                .GetMapper(domainEvent)
                ?.Map(domainEvent);
            if (intergrationEvent != null)
            {
                integrationEvents.Add(intergrationEvent);
            }
        }

        return integrationEvents;
    }
}
