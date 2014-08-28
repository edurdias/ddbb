using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;
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

		public IQueryBuilder CreateBuilder(IDatabaseConnection connection)
		{
			return new DocumentDbQueryBuilder(this, connection);
		}

		public IQueryBuilder CreateBuilder(IDatabaseConnection connection, string collection)
		{
			return CreateBuilder(connection).Using(collection);
		}

		public async Task<IQueryable<dynamic>> Execute(IQueryBuilder queryBuilder)
		{
			return await Task.Run(() => {
				var connection = queryBuilder.GetDatabaseConnection();

				using (var client = CreateClient(connection)) {
					var database = FindDatabase(client, connection.Database).Result;

					if (database == null) {
						throw new InvalidOperationException(string.Format(Resources.DatabaseDoesntExistMessage, connection.Database));
					}

					var collection = FindCollection(client, database, queryBuilder.GetCollection()).Result;

					if (collection == null) {
						throw new InvalidOperationException(string.Format(Resources.DocumentCollectionDoesntExistMessage, queryBuilder.GetCollection()));
					}

					return client.CreateDocumentQuery(collection.DocumentsLink, queryBuilder.GetSqlStatement());
				}
			});
		}

		public async Task<IEnumerable<IDatabaseConnection>> GetDatabases(IConnection connection)
		{
			return await Task.Run(() => {
				using (var client = CreateClient(connection)) {
					return client.CreateDatabaseQuery().AsEnumerable().Select(d => new DocumentDbConnection {
						Database = d.Id,
						AuthorizationKey = connection.AuthorizationKey,
						EndpointUrl = connection.EndpointUrl,
					});
				}
			});
		}

		public async Task<IEnumerable<string>> GetCollections(IDatabaseConnection connection)
		{
			return await Task.Run(() => {
				using (var client = CreateClient(connection)) {
					var database = FindDatabase(client, connection.Database).Result;

					if (database == null) {
						throw new InvalidOperationException(string.Format(Resources.DatabaseDoesntExistMessage, connection.Database));
					}

					return client.CreateDocumentCollectionQuery(database.CollectionsLink).AsEnumerable().Select(c => c.Id);
				}
			});

		}
	}
}
