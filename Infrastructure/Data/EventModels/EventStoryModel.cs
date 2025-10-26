namespace BankMore.Infrastructure.Data.EventModels
{

	public class EventStoreModel
	{
		public string Id { get; set; }
		public string AggregateId { get; set; }
		public string EventType { get; set; }
		public string EventData { get; set; }
		public int Version { get; set; }
		public DateTime OccurredOn { get; set; }
	}
}