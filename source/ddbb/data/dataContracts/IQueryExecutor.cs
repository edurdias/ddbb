using System.Linq;
using System.Threading.Tasks;

namespace ddbb.App.Data.DataContracts
{
	public interface IQueryExecutor
	{
		IQueryBuilder Create(IConnection connection);
		
		IQueryBuilder Create(IConnection connection, string collection);

		Task<IQueryable<dynamic>> GetDatabases(IConnection connection);

		Task<IQueryable<dynamic>> GetCollections(IConnection connection);

		Task<IQueryable<dynamic>> Execute(IQueryBuilder queryBuilder);
	}
}
