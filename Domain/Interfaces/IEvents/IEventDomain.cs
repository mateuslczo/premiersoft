namespace BankMore.Domain.Interfaces.IEvents
{

    public interface IEventDomain
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
        string EventType { get; }
    }
}