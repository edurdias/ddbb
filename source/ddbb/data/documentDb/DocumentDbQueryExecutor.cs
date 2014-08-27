using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using ddbb.App.Data.DataContracts;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace ddbb.App.Data.DocumentDb
{
	[Export(typeof(IQueryExecutor))]
	public class DocumentDbQueryExecutor : IQueryExecutor
	{
		private async Task<Database> FindOrCreateDatabase(DocumentClient client, IConnection connection)
		{
			var database = client.CreateDatabaseQuery().AsEnumerable().FirstOrDefault(db => db.Id == connection.Database);

			if (database == null)
			{
				var resource = await client.CreateDatabaseAsync(new Database
				{
					Id = connection.Database
				});

				database = resource.Resource;
			}

			return database;
		}

		private async Task<DocumentCollection> FindOrCreateCollection(DocumentClient client, Database database, string name)
		{
			var collection = client.CreateDocumentCollectionQuery(database.CollectionsLink).AsEnumerable().FirstOrDefault(c => c.Id == name);

			if (collection == null)
			{
				var resource = await client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection
				{
					Id = name
				});

				collection = resource.Resource;
			}

			return collection;
		}

		public IQueryBuilder Create(IConnection connection)
		{
			return new DocumentDbQueryBuilder(this, connection);
		}

		public IQueryBuilder Create(IConnection connection, string collection)
		{
			return Create(connection).Using(collection);
		}

		public async Task<IQueryable<dynamic>> Execute(IQueryBuilder queryBuilder)
		{
			var connection = queryBuilder.GetConnection();

			using(var client = new DocumentClient(new Uri(connection.Url), connection.Key))
			{
				var database = await FindOrCreateDatabase(client, connection);
				var collection = await FindOrCreateCollection(client, database, queryBuilder.Collection);

				return await Task.Run<IQueryable<dynamic>>(() =>
				{
					return client.CreateDocumentQuery(collection.SelfLink, queryBuilder.SqlStatement);
				});
			}
		}

		public Task<IQueryable<dynamic>> GetDatabases(IConnection connection)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<dynamic>> GetCollections(IConnection connection)
		{
			throw new NotImplementedException();
		}
	}
}
