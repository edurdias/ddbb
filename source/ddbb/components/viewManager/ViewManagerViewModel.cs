using System.ComponentModel.Composition;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.ViewManager
{
	[Export(typeof(IViewManager))]
	public class ViewManagerViewModel : IViewManager
	{
		
	}
}