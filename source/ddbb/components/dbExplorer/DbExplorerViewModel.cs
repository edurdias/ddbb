using System.ComponentModel.Composition;
using Caliburn.Micro;
using ddbb.App.Contracts;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App.Components.DbExplorer
{
	[Export(typeof(IDbExplorer))]
	public class DbExplorerViewModel : Screen, IDbExplorer
	{
	}
}
