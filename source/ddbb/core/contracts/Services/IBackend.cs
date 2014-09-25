using System.Collections.Generic;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Services
{
	public interface IBackend
	{
		IConnection Connect(IConnection connection);

		bool IsValid(string endpointUrl, string authenticationKey);

		IDatabase CreateDatabase(string name);

		IDocumentCollection CreateCollection(string name, IDatabase database);

		IList<IDatabase> GetDatabases(IConnection connection);

		IList<IDocumentCollection> GetCollections(IDatabase database);

		IList<IStoredProcedure> GetStoredProcedures(IDocumentCollection collection);

		IList<ITrigger> GetTriggers(IDocumentCollection collection);

		IList<IUserDefinedFunction> GetUserDefinedFunctions(IDocumentCollection collection);
	}
}