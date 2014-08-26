using System.ComponentModel.Composition;
using Caliburn.Micro;
using ddbb.App.Contracts;

namespace ddbb.App.Components.Shell
{
	[Export(typeof(IShell))]
	public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell
	{
		[ImportingConstructor]
		public ShellViewModel(IDbExplorer explorer, IViewManager manager)
		{
			DbExplorer = explorer;
			ViewManager = manager;
		}

		public IDbExplorer DbExplorer { get; private set; }

		public IViewManager ViewManager { get; private set; }
	}
}
