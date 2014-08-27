using System.Linq;
using System.Threading.Tasks;

namespace ddbb.App.Data.DataContracts
{
	public interface IQueryBuilder
	{
		string Collection { get; }

		string SqlStatement { get; }

		IConnection GetConnection();

		IQueryBuilder Using(string collection);

		Task<IQueryable<dynamic>> Execute(string sql);
	}
}
