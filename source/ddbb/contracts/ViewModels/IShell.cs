namespace ddbb.App.Contracts.ViewModels
{
	public interface IShell
	{
		IDbExplorer DbExplorer { get; }

		IViewManager ViewManager { get; }
	}
}