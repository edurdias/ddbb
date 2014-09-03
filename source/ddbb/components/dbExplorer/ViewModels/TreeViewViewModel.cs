using Caliburn.Micro;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public abstract class TreeViewViewModel : PropertyChangedBase
	{
		private bool _isExpanded;
		private bool _isSelected;

		public bool IsExpanded
		{
			get
			{
				return _isExpanded;
			}
			set
			{
				if (Equals(value, _isExpanded)) {
					return;
				}

				_isExpanded = value;
				NotifyOfPropertyChange(() => IsExpanded);
				ToggleExpansion(value);
				Refresh();
			}
		}

		public bool IsSelected
		{
			get
			{
				return _isSelected;
			}
			set
			{
				if (Equals(value, _isSelected)) {
					return;
				}

				_isSelected = value;
				NotifyOfPropertyChange(() => IsSelected);
				ToggleSelection(value);
				Refresh();
			}
		}

		protected virtual void ToggleExpansion(bool value)
		{
		}

		protected virtual void ToggleSelection(bool value)
		{
		}
	}
}