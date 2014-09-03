using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;
using ddbb.App.Services.DocumentDb.Models;
using ddbb.App.Services.DocumentDb.Properties;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace ddbb.App.Services.DocumentDb
{
	[Export(typeof(IDocumentDbService))]
	public class DocumentDbService : IDocumentDbService
	{
		private static DocumentClient CreateClient(IConnection connection)
		{
			return new DocumentClient(new Uri(connection.EndpointUrl), connection.AuthorizationKey);
		}

		private static async Task<Database> FindDatabase(DocumentClient client, string databaseName, bool create = false)
		{
			var database = client.CreateDatabaseQuery().AsEnumerable().FirstOrDefault(db => db.Id == databaseName);

			if (database == null && create) {
				var resource = await client.CreateDatabaseAsync(new Database {
					Id = databaseName
				});

				database = resource.Resource;
			}

			return database;
		}

		private static async Task<DocumentCollection> FindCollection(DocumentClient client, Database database, string name, bool create = false)
		{
			var collection = client.CreateDocumentCollectionQuery(database.CollectionsLink).AsEnumerable().FirstOrDefault(c => c.Id == name);

			if (collection == null && create) {
				var resource = await client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection {
					Id = name
				});

				collection = resource.Resource;
			}

			return collection;
		}

		private static IEnumerable<IDbSproc> GetStoredProcedures(DocumentClient client, DocumentCollection collection)
		{
			//return client.CreateStoredProcedureQuery(c.StoredProceduresLink).Select();
			return Enumerable.Empty<IDbSproc>();
		}

		private static IEnumerable<IDbTrigger> GetTriggers(DocumentClient client, DocumentCollection collection)
		{
			//return client.CreateStoredProcedureQuery(c.StoredProceduresLink).Select();
			return Enumerable.Empty<IDbTrigger>();
		}

		private static IEnumerable<IDbFunction> GetFunctions(DocumentClient client, DocumentCollection collection)
		{
			//return client.CreateStoredProcedureQuery(c.StoredProceduresLink).Select();
			return Enumerable.Empty<IDbFunction>();
		}

		public bool IsValid(string endpointUrl, string authenticationKey)
		{
			using (var client = CreateClient(new DocumentDbConnection { AuthorizationKey = authenticationKey, EndpointUrl = endpointUrl })) {
				try {
					client.OpenAsync().Wait();
					return true;
				}
				catch (Exception) {
					return false;
				}
			}
		}

		public IQueryBuilder CreateBuilder(IDatabase database)
		{
			return new DocumentDbQueryBuilder(this, database);
		}

		public IQueryBuilder CreateBuilder(IDatabase database, string collection)
		{
			return CreateBuilder(database).Using(collection);
		}

		public async Task<IConnection> Connect(IConnection connection)
		{
			connection.Databases = await GetDatabases(connection);
			return connection;
		}

		public async Task<IEnumerable<dynamic>> Execute(IQueryBuilder queryBuilder)
		{
			return await Task.Run(() => {
				var database = queryBuilder.GetDatabase();
				var connection = queryBuilder.GetConnection();

				using (var client = CreateClient(connection)) {
					var dbDatabase = FindDatabase(client, database.Name).Result;

					if (dbDatabase == null) {
						throw new InvalidOperationException(string.Format(Resources.DatabaseDoesntExistMessage, database.Name));
					}

					var collection = FindCollection(client, dbDatabase, queryBuilder.GetCollection()).Result;

					if (collection == null) {
						throw new InvalidOperationException(string.Format(Resources.DocumentCollectionDoesntExistMessage, queryBuilder.GetCollection()));
					}

					return client.CreateDocumentQuery(collection.DocumentsLink, queryBuilder.GetSqlStatement()).ToList();
				}
			});
		}

		public async Task<IEnumerable<IDatabase>> GetDatabases(IConnection connection)
		{
			return await Task.Run(() => {
				using (var client = CreateClient(connection)) {
					return client.CreateDatabaseQuery().AsEnumerable().Select(d => new DocumentDbDatabase(connection) {
						Name = d.Id
					}).ToList();
				}
			});
		}

		public async Task<IEnumerable<IDbCollection>> GetCollections(IDatabase database)
		{
			return await Task.Run(() => {
				using (var client = CreateClient(database.ParentConnection)) {
					var dbDatabase = FindDatabase(client, database.Name).Result;

					if (dbDatabase == null) {
						throw new InvalidOperationException(string.Format(Resources.DatabaseDoesntExistMessage, database.Name));
					}

					return client.CreateDocumentCollectionQuery(dbDatabase.CollectionsLink).AsEnumerable().Select(c => new DocumentDbCollection {
						Name = c.Id,
						Triggers = GetTriggers(client, c),
						Functions = GetFunctions(client, c),
						StoredProcedures = GetStoredProcedures(client, c),
					}).ToList();
				}
			});
		}
	}
}
