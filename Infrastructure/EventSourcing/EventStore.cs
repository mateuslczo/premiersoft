using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.Infrastructure.EventSourcing.Models;
using BankMore.Domain.Interfaces.IEvents;
using Dapper;
using System.Text.Json;

namespace BankMore.Application.Models.Infrastructure.EventSourcing
{

    public class EventStore : IEventStore
    {
        private readonly DapperContext _context;

        public EventStore(DapperContext context)
        {
            _context = context;
        }

        public async Task AppendEventsAsync(Guid aggregateId, IEnumerable<IEventDomain> events, int expectedVersion)
        {
            const string sql = @"
            INSERT INTO EventStore (Id, AggregateId, EventType, EventData, Version, OccurredOn)
            VALUES (:Id, :AggregateId, :EventType, :EventData, :Version, :OccurredOn)";

            var version = expectedVersion + 1;

            foreach (var evento in events)
            {
                var eventData = JsonSerializer.Serialize(evento, evento.GetType());

                var parameters = new
                {
                    Id = evento.EventId,
                    AggregateId = aggregateId,
                    evento.EventType,
                    EventData = eventData,
                    Version = version++,
                    evento.OccurredOn
                };

                await _context.GetConnection().ExecuteAsync(sql, parameters, _context.Transaction);
            }
        }

        public async Task<IEnumerable<IEventDomain>> GetEventsAsync(Guid aggregateId)
        {
            const string sql = @"
                                    SELECT EventType, EventData, OccurredOn
                                    FROM EventStore 
                                    WHERE AggregateId = :AggregateId 
                                    ORDER BY Version";

            var events = await _context.GetConnection().QueryAsync<EventStoreModel>(sql, new { AggregateId = aggregateId });

            return events.Select(ToDomainEvent);
        }

        public async Task<IEnumerable<IEventDomain>> GetEventsByTypeAsync(string eventType)
        {

            const string sql = @"
                                    SELECT EventType, EventData, OccurredOn
                                    FROM EventStore 
                                    WHERE EventType = :EventType
                                    ORDER BY Version";

            var events = await _context.GetConnection().QueryAsync<EventStoreModel>(sql, new { EventType = eventType });

            var domainEvents = new List<IEventDomain>();

            foreach (var eventStoreModel in events)
            {
                var type = Type.GetType($"BankMore.Domain.Events.{eventStoreModel.EventType}, BankMore.Domain");
                if (type == null)
                    throw new InvalidOperationException($"Tipo de evento não encontrado: {eventStoreModel.EventType}");

                var domainEvent = (IEventDomain)JsonSerializer.Deserialize(eventStoreModel.EventData, type);
                domainEvents.Add(domainEvent);
            }

            return domainEvents;
        }


        private IEventDomain ToDomainEvent(EventStoreModel eventStore)
        {
            var eventType = Type.GetType($"BankMore.Domain.Events.{eventStore.EventType}, BankMore.Domain");

            if (eventType == null)
                throw new InvalidOperationException($"Tipo de evento não encontrado: {eventStore.EventType}");

            return (IEventDomain)JsonSerializer.Deserialize(eventStore.EventData, eventType)!;
        }
    }

}