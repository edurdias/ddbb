using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Services
{
	public interface IDocumentDbService
	{
		bool IsValid(string endpointUrl, string authenticationKey);

		Task<IConnection> Connect(IConnection connection);

		IQueryBuilder CreateBuilder(IDatabase database);

		IQueryBuilder CreateBuilder(IDatabase database, string collection);

		Task<IEnumerable<IDatabase>> GetDatabases(IConnection connection);

		Task<IEnumerable<string>> GetCollections(IDatabase database);

		Task<IQueryable<dynamic>> Execute(IQueryBuilder queryBuilder);
	}
}
