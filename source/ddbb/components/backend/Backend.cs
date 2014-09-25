using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace ddbb.Components.Backend
{

	[Export(typeof(IBackend))]
	public class Backend : IBackend
    {
		public IConnection Connect(IConnection connection)
	    {
		    Client = CreateClient(connection.EndpointUrl, connection.AuthorizationKey);
		    return connection;
	    }

	    private static DocumentClient Client { get; set; }

	    public bool IsValid(string endpointUrl, string authenticationKey)
	    {
		    try
		    {
			    using (var client = CreateClient(endpointUrl, authenticationKey))
				    client.OpenAsync().Wait();
			    return true;
		    }
		    catch (Exception)
		    {
			    return false;
		    }
	    }

	    public IDatabase CreateDatabase(string name)
	    {
		    return new BackendDatabase(Client.CreateDatabaseAsync(new Database {Id = name}).Result.Resource);
	    }

	    public IDocumentCollection CreateCollection(string name, IDatabase database)
	    {
			return new BackendCollection(Client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection { Id = name }).Result.Resource);
	    }

		public IList<IDatabase> GetDatabases(IConnection connection)
		{
			return Client.CreateDatabaseQuery().ToList()
				.Select(db => new BackendDatabase(db))
				.OfType<IDatabase>()
				.ToList();
		}

		public IList<IDocumentCollection> GetCollections(IDatabase database)
		{
			return Client.CreateDocumentCollectionQuery(database.CollectionsLink).ToList()
				.Select(c => new BackendCollection(c))
				.OfType<IDocumentCollection>()
				.ToList();
		}

		public IList<IStoredProcedure> GetStoredProcedures(IDocumentCollection collection)
	    {
		    return Client.CreateStoredProcedureQuery(collection.StoredProceduresLink).ToList()
			    .Select(sp => new BackendStoredProcedure(sp))
				.OfType<IStoredProcedure>()
				.ToList();
	    }

	    public IList<ITrigger> GetTriggers(IDocumentCollection collection)
	    {
		    return Client.CreateTriggerQuery(collection.TriggersLink).ToList()
			    .Select(t => new BackendTrigger(t))
			    .OfType<ITrigger>()
			    .ToList();
	    }

	    public IList<IUserDefinedFunction> GetUserDefinedFunctions(IDocumentCollection collection)
	    {
		    return Client.CreateUserDefinedFunctionQuery(collection.UserDefinedFunctionsLink).ToList()
			    .Select(udf => new BackendUserDefinedFunction(udf))
			    .OfType<IUserDefinedFunction>()
			    .ToList();
	    }

	    private static DocumentClient CreateClient(string endpointUrl, string authorizationKey)
	    {
		    return new DocumentClient(new Uri(endpointUrl), authorizationKey);
	    }
    }
}
