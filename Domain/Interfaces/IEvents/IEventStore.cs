namespace BankMore.Domain.Interfaces.IEvents
{

    public interface IEventStore
    {
        Task AppendEventsAsync(Guid aggregateId, IEnumerable<IEventDomain> events, int expectedVersion);
        Task<IEnumerable<IEventDomain>> GetEventsAsync(Guid aggregateId);
        Task<IEnumerable<IEventDomain>> GetEventsByTypeAsync(string eventType);
    }
}
