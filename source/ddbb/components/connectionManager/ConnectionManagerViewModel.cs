using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Repositories;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.ConnectionManager
{
	[Export(typeof(IConnectionManagerViewModel))]
	public class ConnectionManagerViewModel : Screen, IConnectionManagerViewModel
	{
		private IObservableCollection<IConnection> connections;

		[ImportingConstructor]
		public ConnectionManagerViewModel(IConnectionRepository repository)
		{
			Connections = new BindableCollection<IConnection>(repository.All());
			Connections.CollectionChanged += (sender, args) =>
			{
				switch (args.Action)
				{
					case NotifyCollectionChangedAction.Add:
						repository.Add(args.NewItems.OfType<IConnection>());
						break;
					case NotifyCollectionChangedAction.Remove:
						repository.Remove(args.OldItems.OfType<IConnection>());
						break;
					case NotifyCollectionChangedAction.Replace:
						repository.Update(args.NewItems.OfType<IConnection>());
						break;
				}
			};
		}

		public IObservableCollection<IConnection> Connections
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
