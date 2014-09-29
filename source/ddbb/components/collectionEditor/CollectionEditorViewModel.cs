using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.Components.CollectionView
{
	[Export(typeof(ICollectionEditorViewModel))]
	public class CollectionEditorViewModel : Screen, ICollectionEditorViewModel
	{
		private IDocumentCollection collection;
		private const string StatementTemplate = "SELECT * FROM {0} {1}";

		[ImportingConstructor]
		public CollectionEditorViewModel(IBackend backend)
		{
			Backend = backend;
		}

		public IBackend Backend { get; set; }

		public object Content { get { return Collection;  } set { Collection = value as IDocumentCollection; } }

		public IDocumentCollection Collection
		{
			get { return collection; }
			set
			{
				collection = value;
				DisplayName = collection != null ? collection.Name : string.Empty;
				CreateDefaultStatement();
				NotifyOfPropertyChange(() => Collection);
				Refresh();
			}
		}

		public string Statement { get; set; }

		private void CreateDefaultStatement()
		{
			Statement = string.Format(StatementTemplate, Collection.Name, Collection.Name.ToLower().First());
		}
	}
}