using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Events;

namespace ddbb.App.Components.ConnectionManager.Events
{
	public class ConnectionUpdatedEvent : IConnectionUpdatedEvent
	{
		public ConnectionUpdatedEvent(IConnection connection)
		{
			Connection = connection;
		}

		public IConnection Connection { get; set; }
	}
}