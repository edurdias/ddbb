using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ddbb.App.Data.DataContracts
{
	public interface IQueryExecutor
	{
		IQueryBuilder CreateBuilder(IDatabaseConnection connection);

		IQueryBuilder CreateBuilder(IDatabaseConnection connection, string collection);

		Task<IEnumerable<IDatabaseConnection>> GetDatabases(IConnection connection);

		Task<IEnumerable<string>> GetCollections(IDatabaseConnection connection);

		Task<IQueryable<dynamic>> Execute(IQueryBuilder queryBuilder);
	}
}
