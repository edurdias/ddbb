namespace ddbb.App.Contracts.ViewModels
{
	public interface IShell
	{
		IDbExplorerViewModel DbExplorer { get; }

		IViewManagerViewModel ViewManager { get; }
	}
}