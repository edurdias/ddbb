using System.ComponentModel.Composition;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.Components.CollectionView
{
	[Export(typeof(ICollectionEditorViewModel))]
	public class CollectionEditorViewModel : Screen, ICollectionEditorViewModel
	{
		public CollectionEditorViewModel()
		{
			DisplayName = "Collection Editor";
		}

		public IDbCollection Collection { get; set; }
	}
}