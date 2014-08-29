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

		IQueryBuilder CreateBuilder(IDatabaseConnection connection);

		IQueryBuilder CreateBuilder(IDatabaseConnection connection, string collection);

		Task<IEnumerable<IDatabaseConnection>> GetDatabases(IConnection connection);

		Task<IEnumerable<string>> GetCollections(IDatabaseConnection connection);

		Task<IQueryable<dynamic>> Execute(IQueryBuilder queryBuilder);
	}
}
