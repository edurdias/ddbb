using System;
using System.Collections.Generic;
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
						repository.Add(args.NewItems.OfType<IConnection>().ToList());
						break;
					case NotifyCollectionChangedAction.Remove:
						repository.Remove(args.OldItems.OfType<IConnection>().ToList());
						break;
					case NotifyCollectionChangedAction.Replace:
						var items = new Dictionary<IConnection, IConnection>();
						for (var i = 0; i < args.OldStartingIndex; i++)
							items.Add((IConnection)args.OldItems[i], (IConnection)args.NewItems[i]);
						repository.Update(items);
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
