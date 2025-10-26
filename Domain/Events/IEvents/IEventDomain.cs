namespace BankMore.Domain.Events.IEvents
{

    public interface IEventDomain
	{
        Guid EventId { get; }
        DateTime OccurredOn { get; }
        string EventType { get; }
    }
}