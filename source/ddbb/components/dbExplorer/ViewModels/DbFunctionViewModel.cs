using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbFunctionViewModel : TreeViewViewModel
	{
		private readonly IDbFunction _function;

		public DbFunctionViewModel(IDbFunction function)
		{
			_function = function ?? new DummyFunction();
		}

		public string Script
		{
			get
			{
				return _function.Script;
			}
			set
			{
				if (Equals(value, _function.Script)) {
					return;
				}

				_function.Script = value;
				NotifyOfPropertyChange(() => Script);
				Refresh();
			}
		}

		protected override void ToggleExpansion(bool value)
		{

		}

		class DummyFunction : IDbFunction
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