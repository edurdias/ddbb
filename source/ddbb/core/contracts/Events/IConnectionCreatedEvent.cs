using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Events
{
	public interface IConnectionCreatedEvent
	{
		IConnection Connection { get; set; }
	}
}