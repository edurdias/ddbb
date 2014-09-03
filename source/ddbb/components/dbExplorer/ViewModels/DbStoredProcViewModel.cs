using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbStoredProcViewModel : TreeViewViewModel
	{
		private readonly IDbSproc _sproc;

		public DbStoredProcViewModel(IDbSproc sproc)
		{
			_sproc = sproc ?? new DummyStoredProc();
		}

		public string Script
		{
			get
			{
				return _sproc.Script;
			}
			set
			{
				if (Equals(value, _sproc.Script)) {
					return;
				}

				_sproc.Script = value;
				NotifyOfPropertyChange(() => Script);
				Refresh();
			}
		}

		protected override void ToggleExpansion(bool value)
		{

		}
		
		class DummyStoredProc : IDbSproc
		{
			public string Script
			{
				get
				{
					return string.Empty;
				}
				set
				{
				}
			}
		}
	}
}