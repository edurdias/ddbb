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
			return new BackendDatabase(Client.CreateDatabaseAsync(new Database { Id = name }).Result.Resource);
		}

		public IDocumentCollection CreateCollection(string name, IDatabase database)
		{
			return new BackendCollection(Client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection { Id = name }).Result.Resource);
		}

		public IEnumerable<IDatabase> GetDatabases(IConnection connection)
		{
			return Client.CreateDatabaseQuery()
				.ToList() //TODO:Review Linq exception : Only value types and anonymous types are supported in constructors
				.Select(db => new BackendDatabase(db))
				.ToList();
		}

		public IEnumerable<IDocumentCollection> GetCollections(IDatabase database)
		{
			return Client.CreateDocumentCollectionQuery(database.CollectionsLink)
				.ToList() //TODO:Review Linq exception : Only value types and anonymous types are supported in constructors
				.Select(c => new BackendCollection(c))
				.ToList();
		}

		public IEnumerable<IStoredProcedure> GetStoredProcedures(IDocumentCollection collection)
		{
			return Client.CreateStoredProcedureQuery(collection.StoredProceduresLink).Select(sp => new BackendStoredProcedure(sp));
		}

		public IEnumerable<ITrigger> GetTriggers(IDocumentCollection collection)
		{
			return Client.CreateTriggerQuery(collection.TriggersLink).Select(t => new BackendTrigger(t));
		}

		public IEnumerable<IUserDefinedFunction> GetUserDefinedFunctions(IDocumentCollection collection)
		{
			return Client.CreateUserDefinedFunctionQuery(collection.UserDefinedFunctionsLink)
					.Select(udf => new BackendUserDefinedFunction(udf));
		}

		private static DocumentClient CreateClient(string endpointUrl, string authorizationKey)
		{
			return new DocumentClient(new Uri(endpointUrl), authorizationKey);
		}
	}
}
