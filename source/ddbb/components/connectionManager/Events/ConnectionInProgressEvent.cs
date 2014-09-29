using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Events;

namespace ddbb.App.Components.ConnectionManager.Events
{
	public class ConnectionInProgressEvent : IConnectionInProgressEvent
	{
		public ConnectionInProgressEvent(IConnection connection)
		{
			Connection = connection;
		}

		public IConnection Connection { get; set; }
	}
}