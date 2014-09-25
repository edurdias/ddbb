using System.ComponentModel.Composition;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.Components.CollectionView
{
	[Export(typeof(ICollectionEditorViewModel))]
	public class CollectionEditorViewModel : Screen, ICollectionEditorViewModel
	{
		private IDocumentCollection collection;

		public CollectionEditorViewModel()
		{
			Collection = Content as IDocumentCollection;
			DisplayName = Collection != null ? Collection.Name : string.Empty;
		}

		public object Content { get { return Collection;  } set { Collection = value as IDocumentCollection; } }

		public IDocumentCollection Collection
		{
			get { return collection; }
			set
			{
				collection = value;
				DisplayName = collection != null ? collection.Name : string.Empty;
				NotifyOfPropertyChange(() => Collection);
				Refresh();
			}
		}
	}
}