using System.ComponentModel.Composition;
using ddbb.App.Contracts;

namespace ddbb.App.Components.TabManager
{
	[Export(typeof(IViewManager))]
	public class ViewManagerViewModel : IViewManager
	{
		
	}
}