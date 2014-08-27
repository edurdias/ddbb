using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Documents;
using Caliburn.Micro;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.ConnectionManager
{
	[Export(typeof(IConnectionManagerViewModel))]
	public class ConnectionManagerViewModel : Screen, IConnectionManagerViewModel
	{
		private IObservableCollection<Connection> connections;

		public ConnectionManagerViewModel()
		{
			// creating a Mock of connections
			Connections = new BindableCollection<Connection>(new List<Connection>
			{
				new Connection { Name = "local", EndpointUrl = "localhost", AuthorizationKey = "development" }
			});
		}

		public IObservableCollection<Connection> Connections
		{
			get { return connections; }
			set
			{
				if (Equals(value, connections)) return;
				connections = value;
				NotifyOfPropertyChange(()=> Connections);
			}
		}
	}
}
