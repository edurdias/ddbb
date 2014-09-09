using System.ComponentModel.Composition;
using System.Windows;
using ddbb.App.Contracts.Services;

namespace ddbb.App.Infrastructure.Services
{
	[Export(typeof(IMessageService))]
	public class DefaultMessageService : IMessageService
	{
		public MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
		{
			return MessageBox.Show(text, caption, buttons, icon);
		}
	}
}