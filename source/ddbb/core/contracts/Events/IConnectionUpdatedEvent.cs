using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Events
{
	public interface IConnectionUpdatedEvent
	{
		IConnection Connection { get; set; }
	}
}