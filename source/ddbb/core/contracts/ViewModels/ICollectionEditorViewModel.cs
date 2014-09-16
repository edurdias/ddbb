using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.ViewModels
{
	public interface ICollectionEditorViewModel : IContentView
	{
		IDbCollection Collection { get; set; }
	}
}