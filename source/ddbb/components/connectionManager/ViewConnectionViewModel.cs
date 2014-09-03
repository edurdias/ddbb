using System;
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using ddbb.App.Components.ConnectionManager.Events;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Repositories;
using ddbb.App.Contracts.Services;

namespace ddbb.App.Components.ConnectionManager
{
	public class ViewConnectionViewModel : Screen
	{
		private string name;
		private string endpointUrl;
		private string authenticationKey;

		public ViewConnectionViewModel(ViewConnectionMode mode, IConnection connection = null)
		{
			if (mode == ViewConnectionMode.Modify && connection == null)
				throw new ArgumentNullException("connection");

			Mode = mode;
			Connection = connection;

			if (Mode == ViewConnectionMode.Modify && Connection != null)
			{
				Name = Connection.Name;
				EndpointUrl = Connection.EndpointUrl;
				AuthenticationKey = Connection.AuthorizationKey;
			}
		}

		[Import]
		public IDocumentDbService DocumentDbService { get; set; }

		[Import]
		public IConnectionRepository Repository { get; set; }

		[Import]
		public IEventAggregator EventAggregator { get; set; }

		public ViewConnectionMode Mode { get; set; }

		public IConnection Connection { get; set; }

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				NotifyOfPropertyChange(() => Name);
				Refresh();
			}
		}

		public string EndpointUrl
		{
			get { return endpointUrl; }
			set
			{
				endpointUrl = value;
				NotifyOfPropertyChange(() => EndpointUrl);
				Refresh();
			}
		}

		public string AuthenticationKey
		{
			get { return authenticationKey; }
			set
			{
				authenticationKey = value;
				NotifyOfPropertyChange(() => AuthenticationKey);
				Refresh();
			}
		}

		public bool CanTestConnection
		{
			get
			{
				return !string.IsNullOrEmpty(EndpointUrl) && !string.IsNullOrEmpty(AuthenticationKey);
			}
		}

		public bool CanSave
		{
			get { return IsValid(); }
		}

		public void TestConnection()
		{
			if (DocumentDbService.IsValid(EndpointUrl, AuthenticationKey))
				MessageBox.Show("It works!");
			else
				MessageBox.Show("Invalid Endpoint Url or Authentication Key", 
					string.Empty,
					MessageBoxButton.OK,
					MessageBoxImage.Warning);
		}

		public void Save()
		{
			if (Mode == ViewConnectionMode.Create)
				SaveOnCreate();
			else
				SaveOnModify();
			TryClose();
		}

		public void Cancel()
		{
			TryClose();
		}

		private void SaveOnCreate()
		{
			var connection = IoC.Get<IConnection>();
			connection.Name = Name;
			connection.EndpointUrl = EndpointUrl;
			connection.AuthorizationKey = AuthenticationKey;
			Repository.Add(connection);
			EventAggregator.PublishOnUIThread(new ConnectionCreatedEvent(connection));
		}

		private void SaveOnModify()
		{
			Connection.Name = Name;
			Connection.EndpointUrl = EndpointUrl;
			Connection.AuthorizationKey = AuthenticationKey;
			Repository.Update(Connection);
			EventAggregator.PublishOnUIThread(new ConnectionUpdatedEvent(Connection));
		}


		private bool IsValid()
		{
			return !string.IsNullOrEmpty(Name)
			       && !string.IsNullOrEmpty(EndpointUrl)
			       && !string.IsNullOrEmpty(AuthenticationKey);
		}
	}
}