using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.Toolbar
{
	[Export(typeof(IToolbarViewModel))]
	public class ToolbarViewModel : IToolbarViewModel
	{
		[ImportingConstructor]
		public ToolbarViewModel(IWindowManager windowManager)
		{
			WindowManager = windowManager;
		}

		public IWindowManager WindowManager { get; set; }

		public void OpenConnectionManager()
		{
			WindowManager.ShowDialog(IoC.Get<IConnectionManagerViewModel>(), null, new Dictionary<string, object>
			{
				{"ResizeMode", ResizeMode.NoResize}
			});
		}
	}
}