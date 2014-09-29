using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using ddbb.App.Contracts.Events;
using ddbb.App.Contracts.Services;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.ViewManager
{
	[Export(typeof(IViewManagerViewModel))]
	public class ViewManagerViewModel : Conductor<IContentView>.Collection.OneActive, IViewManagerViewModel, IHandle<IContentOpenedEvent>, IHandle<IConnectionEstablishedEvent>
	{
		[ImportingConstructor]
		public ViewManagerViewModel(IBackend backend, IEventAggregator eventAggregator)
		{
			Backend = backend;
			eventAggregator.Subscribe(this);
		}

		public IBackend Backend { get; set; }

		public void Handle(IContentOpenedEvent message)
		{
			if (message != null && message.Content != null)
			{
				var contentView = (IContentView) IoC.GetInstance(message.Type, null);
				if(contentView != null)
				{
					contentView.Content = message.Content;
					ActivateItem(contentView);
				}
			}
		}

		public void Handle(IConnectionEstablishedEvent message)
		{
			var databases = message.Connection.Databases;
			var collection = databases.First().Collections.FirstOrDefault();

			var contentView = (IContentView)IoC.GetInstance(typeof(ICollectionEditorViewModel), null);
			contentView.Content = collection;

			ActivateItem(contentView);
		}

	}
}