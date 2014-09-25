using System.Linq;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbDatabaseViewModel : TreeViewViewModel
	{
		private readonly IDatabase _database;
		private readonly IBackend _service;

		private bool _hasLoaded;
		private IObservableCollection<DbUserViewModel> _users;
		private IObservableCollection<DbCollectionViewModel> _collections;

		public DbDatabaseViewModel(IDatabase database, IBackend service)
		{
			_database = database; 
			_service = service;

			Collections = new BindableCollection<DbCollectionViewModel>();

			if (_database.Collections != null && _database.Collections.Any()) {
				Collections.AddRange(_database.Collections.Select(c => new DbCollectionViewModel(c)));
			}
			else {
				Collections.AddRange(new[] { new DbCollectionViewModel(null) });
			}

			Users = new BindableCollection<DbUserViewModel>();

			if (_database.Users != null && _database.Users.Any()) {
				Users.AddRange(_database.Users.Select(u => new DbUserViewModel(u)));
			}
			else {
				Users.AddRange(new[] { new DbUserViewModel(null) });
			}
		}

		public string Name
		{
			get
			{
				return _database.Name;
			}
			set
			{
				if (Equals(value, _database.Name)) {
					return;
				}
				
				_database.Name = value;
				NotifyOfPropertyChange(() => Name);
				Refresh();
			}
		}

		public IObservableCollection<DbCollectionViewModel> Collections
		{
			get
			{
				return _collections;
			}
			set
			{
				if (Equals(value, _collections)) {
					return;
				}

				_collections = value;
				NotifyOfPropertyChange(() => Collections);
				Refresh();
			}
		}

		public IObservableCollection<DbUserViewModel> Users
		{
			get
			{
				return _users;
			}
			set
			{
				if (Equals(value, _users)) {
					return;
				}

				_users = value;
				NotifyOfPropertyChange(() => Users);
				Refresh();
			}
		}

		protected override void ToggleExpansion(bool value)
		{
			if (value && !_hasLoaded) {
				Collections.Clear();

				//_service.GetCollections(_database).ContinueWith(task => {

				//	Collections.AddRange(task.Result.Select(c => new DbCollectionViewModel(c)));

				//	_hasLoaded = true;
				//});
			}
		}
	}
}