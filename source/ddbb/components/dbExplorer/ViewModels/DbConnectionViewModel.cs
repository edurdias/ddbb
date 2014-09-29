using System.Linq;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbConnectionViewModel : TreeViewViewModel
	{
		private readonly IConnection _connection;
		private IObservableCollection<DbDatabaseViewModel> _databases;

		public DbConnectionViewModel(IConnection connection, IBackend service)
		{
			_connection = connection;
			Databases = new BindableCollection<DbDatabaseViewModel>();
		}

		public long Id
		{
			get
			{
				return _connection.Id;
			}
		}

		public string Name
		{
			get
			{
				return _connection.Name;
			}
			set
			{
				if (Equals(value, _connection.Name)) {
					return;
				}

				_connection.Name = value;
				NotifyOfPropertyChange(() => Name);
				Refresh();
			}
		}

		public string AuthorizationKey
		{
			get
			{
				return _connection.AuthorizationKey;
			}
			set
			{
				if (Equals(value, _connection.AuthorizationKey)) {
					return;
				}

				_connection.AuthorizationKey = value;
				NotifyOfPropertyChange(() => AuthorizationKey);
				Refresh();
			}
		}

		public string EndpointUrl
		{
			get
			{
				return _connection.EndpointUrl;
			}
			set
			{
				if (Equals(value, _connection.EndpointUrl)) {
					return;
				}

				_connection.EndpointUrl = value;
				NotifyOfPropertyChange(() => EndpointUrl);
				Refresh();
			}
		}

		public IObservableCollection<DbDatabaseViewModel> Databases
		{
			get
			{
				return _databases;
			}
			set
			{
				if (Equals(value, _databases)) {
					return;
				}

				_databases = value;
				NotifyOfPropertyChange(() => Databases);
				Refresh();
			}
		}
	}
}