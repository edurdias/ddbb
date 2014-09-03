using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbUserViewModel : TreeViewViewModel
	{
		private readonly IDbUser _user;

		public DbUserViewModel(IDbUser user)
		{
			_user = user;
		}

		public string UserName
		{
			get
			{
				if (_user == null) {
					return string.Empty;
				}
				return _user.Username;
			}
			set
			{
				if (_user == null) {
					return;
				}

				if (Equals(value, _user.Username)) {
					return;
				}

				_user.Username = value;
				NotifyOfPropertyChange(() => UserName);
				Refresh();
			}
		}
	}
}