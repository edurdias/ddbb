using System.Collections.Generic;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbTriggerViewModel : TreeViewViewModel
	{
		private readonly ITrigger _trigger;

		public DbTriggerViewModel(ITrigger trigger)
		{
			_trigger = trigger ?? new DummyTrigger();
		}

		public string Script
		{
			get
			{
				return _trigger.Body;
			}
			set
			{
				if (Equals(value, _trigger.Body)) {
					return;
				}

				_trigger.Body = value;
				NotifyOfPropertyChange(() => Script);
				Refresh();
			}
		}

		class DummyTrigger : ITrigger
		{
			public string Body
			{
				get { return string.Empty; }
				set
				{
					
				}
			}

			public string SelfLink { get; set; }
		}
	}
}