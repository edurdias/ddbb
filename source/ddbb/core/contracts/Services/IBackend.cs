using System.Collections.Generic;
using System.Linq;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Services
{
	public interface IBackend
	{
		IConnection Connect(IConnection connection);

		bool IsValid(string endpointUrl, string authenticationKey);

		IDatabase CreateDatabase(string name);

		IDocumentCollection CreateCollection(string name, IDatabase database);

		IEnumerable<IDatabase> GetDatabases(IConnection connection);

		IEnumerable<IDocumentCollection> GetCollections(IDatabase database);

		IEnumerable<IStoredProcedure> GetStoredProcedures(IDocumentCollection collection);

		IEnumerable<ITrigger> GetTriggers(IDocumentCollection collection);

		IEnumerable<IUserDefinedFunction> GetUserDefinedFunctions(IDocumentCollection collection);
	}
}