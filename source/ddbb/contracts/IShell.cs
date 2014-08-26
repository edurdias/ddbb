namespace ddbb.App.Contracts
{
	public interface IShell
	{
		IDbExplorer DbExplorer { get; }

		IViewManager ViewManager { get; }
	}
}