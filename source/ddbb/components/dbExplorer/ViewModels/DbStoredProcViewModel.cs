using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbStoredProcViewModel : TreeViewViewModel
	{
		private readonly IStoredProcedure _sproc;

		public DbStoredProcViewModel(IStoredProcedure sproc)
		{
			_sproc = sproc ?? new DummyStoredProc();
		}

		public string Script
		{
			get
			{
				return _sproc.Body;
			}
			set
			{
				if (Equals(value, _sproc.Body)) {
					return;
				}

				_sproc.Body = value;
				NotifyOfPropertyChange(() => Script);
				Refresh();
			}
		}

		protected override void ToggleExpansion(bool value)
		{

		}
		
		class DummyStoredProc : IStoredProcedure
		{
			public string Body
			{
				get
				{
					return string.Empty;
				}
				set
				{
				}
			}

			public string SelfLink { get; set; }
		}
	}
}