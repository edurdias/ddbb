using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Events;

namespace ddbb.App.Components.ConnectionManager.Events
{
	public class ConnectionCreatedEvent : IConnectionCreatedEvent
	{
		public ConnectionCreatedEvent(IConnection connection)
		{
			Connection = connection;
		}

		public IConnection Connection { get; set; }
	}
}