using System.Linq;
using System.Threading.Tasks;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Services
{
	public interface IQueryBuilder
	{
		string GetCollection();

		string GetSqlStatement();

		IDatabase GetDatabase();

		IConnection GetConnection();

		IQueryBuilder Using(string collection);

		Task<IQueryable<dynamic>> Execute(string sql);
	}
}
