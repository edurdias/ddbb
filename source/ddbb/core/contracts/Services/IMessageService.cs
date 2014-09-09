using System.Windows;

namespace ddbb.App.Contracts.Services
{
	public interface IMessageService
	{
		MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon);
	}
}