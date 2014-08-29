using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using ddbb.App.Components.ConnectionManager.Events;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Events;
using ddbb.App.Contracts.Repositories;
using ddbb.App.Contracts.Services;
using ddbb.App.Contracts.ViewModels;
using ddbb.App.Domain;

namespace ddbb.App.Components.ConnectionManager
{
	[Export(typeof(IConnectionManagerViewModel))]
	public class ConnectionManagerViewModel : Screen, IConnectionManagerViewModel, IHandle<IConnectionCreatedEvent>, IHandle<IConnectionUpdatedEvent>
	{
		private IObservableCollection<IConnection> connections;
		private IConnection selectedConnection;

		[ImportingConstructor]
		public ConnectionManagerViewModel(IWindowManager windowManager, IConnectionRepository repository, IEventAggregator eventAggregator, IDocumentDbService documentDb)
		{
			WindowManager = windowManager;
			Repository = repository;
			Connections = new BindableCollection<IConnection>(Repository.All());
			EventAggregator = eventAggregator;
			EventAggregator.Subscribe(this);
			DocumentDb = documentDb;
		}

		private IWindowManager WindowManager { get; set; }

		public IConnectionRepository Repository { get; set; }

		private IEventAggregator EventAggregator { get; set; }

		public IDocumentDbService DocumentDb { get; set; }

		public IObservableCollection<IConnection> Connections
		{
			get { return connections; }
			set
			{
				if (Equals(value, connections)) return;
				connections = value;
				NotifyOfPropertyChange(()=> Connections);
				Refresh();
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
			get { return SelectedConnection != null; }
		}

		public bool CanRemove
		{
			get { return SelectedConnection != null; }
		}

		public bool CanCopy
		{
			get { return SelectedConnection != null; }
		}

		public bool CanConnect
		{
			get { return SelectedConnection != null;  }
		}

		public void Create()
		{
			OpenDialog(new ViewConnectionViewModel(ViewConnectionMode.Create));
		}

		public void Modify()
		{
			OpenDialog(new ViewConnectionViewModel(ViewConnectionMode.Modify, SelectedConnection));
		}

		public void Remove()
		{
			var result = MessageBox.Show(
				string.Format("Really delete {0} connection?", SelectedConnection.Name), 
				null, // no caption
				MessageBoxButton.OKCancel, 
				MessageBoxImage.Question);
			if (result == MessageBoxResult.OK)
			{
				Repository.Remove(SelectedConnection);
				Connections.Remove(SelectedConnection);
			}
		}

		public void Copy()
		{
			OpenDialog(new CopyConnectionViewModel(SelectedConnection));
		}

		public void Connect()
		{
			var establishedConnection = DocumentDb.Connect(SelectedConnection);
			EventAggregator.PublishOnUIThread(new ConnectionEstablishedEvent(establishedConnection));
			TryClose();
		}

		private void OpenDialog(Screen viewModel)
		{
			IoC.BuildUp(viewModel);
			WindowManager.ShowDialog(viewModel, null, new Dictionary<string, object>
			{
				{"ResizeMode", ResizeMode.NoResize}
			});
		}

		protected override void OnDeactivate(bool close)
		{
			EventAggregator.Unsubscribe(this);
			base.OnDeactivate(close);
		}

		public void Handle(IConnectionCreatedEvent message)
		{
			Connections.Add(message.Connection);
			NotifyOfPropertyChange(() => Connections);
		}

		public void Handle(IConnectionUpdatedEvent message)
		{
			Connections.Clear();
			Connections.AddRange(Repository.All());
			NotifyOfPropertyChange(() => Connections);
		}
	}
}
