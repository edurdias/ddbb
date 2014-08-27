using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
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
		private IConnection selectedConnection;

		[ImportingConstructor]
		public ConnectionManagerViewModel(IWindowManager windowManager, IConnectionRepository repository)
		{
			WindowManager = windowManager;
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

		private IWindowManager WindowManager { get; set; }

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

		public IConnection SelectedConnection
		{
			get { return selectedConnection; }
			set
			{
				selectedConnection = value;
				NotifyOfPropertyChange(() => SelectedConnection);
				Refresh();
			}
		}

		public bool CanModify
		{
			get
			{
				return SelectedConnection != null;
			}
		}

		public bool CanRemove
		{
			get
			{
				return SelectedConnection != null;
			}
		}

		public bool CanCopy
		{
			get
			{
				return SelectedConnection != null;
			}
		}

		public void Create()
		{
			WindowManager.ShowDialog(new CreateConnectionViewModel(), null, new Dictionary<string, object>
			{
				{"ResizeMode", ResizeMode.NoResize}
			});
		}

		public void Modify()
		{
			MessageBox.Show("modify");
		}

		public void Remove()
		{
			MessageBox.Show("remove");
		}

		public void Copy()
		{
			MessageBox.Show("copy");
		}
	}
}
