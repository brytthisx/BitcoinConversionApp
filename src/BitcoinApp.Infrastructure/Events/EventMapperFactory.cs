using BitcoinApp.Domain;

namespace BitcoinApp.Infrastructure.Events;

public class EventMapperFactory(Dictionary<Type, IEventMapper> mappers)
{
    public IEventMapper GetMapper(IDomainEvent domainEvent)
    {
        if (mappers.TryGetValue(domainEvent.GetType(), out IEventMapper? mapper))
        {
            return mapper;
        }

        return null;
    }
}
