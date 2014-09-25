using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbFunctionViewModel : TreeViewViewModel
	{
		private readonly IUserDefinedFunction _function;

		public DbFunctionViewModel(IUserDefinedFunction function)
		{
			_function = function ?? new DummyFunction();
		}

		public string Script
		{
			get
			{
				return _function.Body;
			}
			set
			{
				if (Equals(value, _function.Body)) {
					return;
				}

				_function.Body = value;
				NotifyOfPropertyChange(() => Script);
				Refresh();
			}
		}

		protected override void ToggleExpansion(bool value)
		{

		}

		class DummyFunction : IUserDefinedFunction
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