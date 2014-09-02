using System.ComponentModel.Composition;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.ViewManager
{
	[Export(typeof(IViewManagerViewModel))]
	public class ViewManagerViewModel : IViewManagerViewModel
	{
		
	}
}