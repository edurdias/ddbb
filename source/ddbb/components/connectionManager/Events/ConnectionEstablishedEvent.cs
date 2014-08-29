using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Events;

namespace ddbb.App.Components.ConnectionManager.Events
{
	public class ConnectionEstablishedEvent : IConnectionEstablishedEvent
	{
		public ConnectionEstablishedEvent(IConnection connection)
		{
			Connection = connection;
		}

		public IConnection Connection { get; private set; }
	}
}