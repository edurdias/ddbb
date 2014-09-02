using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.Shell
{
	[Export(typeof(IShell))]
	public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell
	{
		[ImportingConstructor]
		public ShellViewModel(IToolbarViewModel toolbar, IDbExplorerViewModel explorer, IViewManagerViewModel viewManager, IWindowManager windowManager)
		{
			Toolbar = toolbar;
			DbExplorer = explorer;
			ViewManager = viewManager;
			WindowManager = windowManager;
		}

		public IToolbarViewModel Toolbar { get; set; }

		public IDbExplorerViewModel DbExplorer { get; private set; }

		public IViewManagerViewModel ViewManager { get; private set; }

		private IWindowManager WindowManager { get; set; }

		protected override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			WindowManager.ShowDialog(IoC.Get<IConnectionManagerViewModel>(), null, new Dictionary<string, object>
			{
				{"ResizeMode", ResizeMode.NoResize}
			});

		}
	}
}
