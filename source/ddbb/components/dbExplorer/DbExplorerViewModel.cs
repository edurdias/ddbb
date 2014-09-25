using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using ddbb.App.Components.DbExplorer.ViewModels;
using ddbb.App.Contracts.Events;
using ddbb.App.Contracts.Services;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.DbExplorer
{
	[Export(typeof(IDbExplorerViewModel))]
	public class DbExplorerViewModel : Screen, IDbExplorerViewModel, IHandle<IConnectionEstablishedEvent>
	{
		private IObservableCollection<DbConnectionViewModel> _connections;

		[ImportingConstructor]
		public DbExplorerViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, IBackend documentDb)
		{
			WindowManager = windowManager;
			EventAggregator = eventAggregator;
			DocumentDb = documentDb;
			Connections = new BindableCollection<DbConnectionViewModel>();
			eventAggregator.Subscribe(this);
		}

		protected IWindowManager WindowManager { get; set; }

		protected IEventAggregator EventAggregator { get; set; }

		protected IBackend DocumentDb { get; set; }

		public IObservableCollection<DbConnectionViewModel> Connections
		{
			get
			{
				return _connections;
			}
			set
			{
				if (Equals(value, _connections)) {
					return;
				}

				_connections = value;
				NotifyOfPropertyChange(() => Connections);
				Refresh();
			}
		}

		//public IConnection SelectedConnection
		//{
		//	get
		//	{
		//		return selectedConnection;
		//	}
		//	set
		//	{
		//		selectedConnection = value;
		//		NotifyOfPropertyChange(() => SelectedConnection);
		//		Refresh();
		//	}
		//}


		protected override void OnDeactivate(bool close)
		{
			EventAggregator.Unsubscribe(this);
			base.OnDeactivate(close);
		}

		public void Handle(IConnectionEstablishedEvent message)
		{
			if (Connections.Any(c => c.Id == message.Connection.Id)) {
				return;
			}

			Connections.AddRange(new[] { new DbConnectionViewModel(message.Connection, DocumentDb) });
		}
	}
}
