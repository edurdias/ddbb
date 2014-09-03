using System.Collections.Generic;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbTriggerViewModel : TreeViewViewModel
	{
		private readonly IDbTrigger _trigger;

		public DbTriggerViewModel(IDbTrigger trigger)
		{
			_trigger = trigger ?? new DummyTrigger();
		}

		public string Script
		{
			get
			{
				return _trigger.Script;
			}
			set
			{
				if (Equals(value, _trigger.Script)) {
					return;
				}

				_trigger.Script = value;
				NotifyOfPropertyChange(() => Script);
				Refresh();
			}
		}

		class DummyTrigger : IDbTrigger
		{
			public string Script
			{
				get { return string.Empty; }
				set
				{
					
				}
			}
		}
	}
}