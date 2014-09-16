using System.Collections.Specialized;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using ddbb.App.Contracts.Events;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.ViewManager
{
	[Export(typeof(IViewManagerViewModel))]
	public class ViewManagerViewModel : Conductor<IContentView>.Collection.OneActive, IViewManagerViewModel, IHandle<ICollectionOpenedEvent>
	{
		protected override void OnViewLoaded(object view)
		{
			ActivateItem(IoC.Get<ICollectionEditorViewModel>());
			base.OnViewLoaded(view);
		}

		public void Handle(ICollectionOpenedEvent message)
		{
			if (message != null && message.Collection != null)
			{
				var collectionEditor = IoC.Get<ICollectionEditorViewModel>();
				collectionEditor.Collection = message.Collection;
				ActivateItem(collectionEditor);
			}
		}
	}
}