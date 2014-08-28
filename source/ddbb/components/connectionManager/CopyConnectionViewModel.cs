using System.ComponentModel.Composition;
using Caliburn.Micro;
using ddbb.App.Components.ConnectionManager.Events;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Repositories;

namespace ddbb.App.Components.ConnectionManager
{
	public class CopyConnectionViewModel : Screen
	{
		public CopyConnectionViewModel(IConnection connection)
		{
			Connection = connection;
			Name = string.Format("{0} - Copy", connection.Name);
		}

		[Import]
		public IConnectionRepository Repository { get; set; }

		[Import]
		public IEventAggregator EventAggregator { get; set; }

		public IConnection Connection { get; set; }

		public string Name { get; set; }

		public bool CanSave
		{
			get { return !string.IsNullOrEmpty(Name) && Connection != null; }
		}

		public void Save()
		{
			var connection = IoC.Get<IConnection>();
			connection.Name = Name;
			connection.EndpointUrl = Connection.EndpointUrl;
			connection.AuthorizationKey = Connection.AuthorizationKey;
			Repository.Add(connection);
			EventAggregator.PublishOnUIThread(new ConnectionCreatedEvent(connection));
			TryClose();
		}

		public void Cancel()
		{
			TryClose();
		}
	}
}