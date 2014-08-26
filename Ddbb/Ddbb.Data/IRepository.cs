using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;


namespace Ddbb.Data
{
	public interface IRepository
	{
		IQueryBuilder Create();
		
		IQueryBuilder Create(string collection);

		Task<IQueryable<dynamic>> Execute(IQueryBuilder queryBuilder);
	}

	public class Repository : IRepository
	{
		private IConnection _connection;
		private DocumentClient _client;
		private Database _database;

		public Repository(IConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}

			_connection = connection;
			_client = new DocumentClient(new Uri(connection.Url), connection.Key);
		}

		protected Database Database
		{
			get
			{
				if (_database == null) {
					_database = FindOrCreateDatabase(_connection.Database).Result;
				}

				return _database;
			}
			set
			{
				_database = value;
			}
		}

		protected DocumentClient Client
		{
			get
			{
				return _client;
			}
			set
			{
				_client = value;
			}
		}

		private async Task<Database> FindOrCreateDatabase(string name)
		{
			var database = Client.CreateDatabaseQuery().AsEnumerable().FirstOrDefault(db => db.Id == name);

			if (database == null) {
				var resource = await Client.CreateDatabaseAsync(new Database {
					Id = name
				});

				database = resource.Resource;
			}

			return database;
		}

		private async Task<DocumentCollection> FindOrCreateCollection(string name)
		{
			var collection = Client.CreateDocumentCollectionQuery(Database.CollectionsLink).AsEnumerable().FirstOrDefault(c => c.Id == name);

			if (collection == null) {
				var resource = await _client.CreateDocumentCollectionAsync(Database.SelfLink, new DocumentCollection {
					Id = name
				});

				collection = resource.Resource;
			}

			return collection;
		}

		public IQueryBuilder Create()
		{
			return new QueryBuilder(this);
		}

		public IQueryBuilder Create(string collection)
		{
			return new QueryBuilder(this).Using(collection);
		}

		public async Task<IQueryable<dynamic>> Execute(IQueryBuilder queryBuilder)
		{
			var collection = await FindOrCreateCollection(queryBuilder.Collection);

			return await Task.Run<IQueryable<dynamic>>(() => {
				return Client.CreateDocumentQuery(collection.SelfLink, queryBuilder.SqlStatement);
			});
		}
	}

	public interface IQueryBuilder
	{
		string Collection { get; }

		string SqlStatement { get; }

		IQueryBuilder Using(string collection);

		Task<IQueryable<dynamic>> Execute(string sql);
	}

	public class QueryBuilder : IQueryBuilder
	{
		public QueryBuilder(IRepository repository)
		{
			Repository = repository;
		}

		public string Collection { get; private set; }

		public string SqlStatement { get; private set; }

		protected IRepository Repository { get; set; }

		public IQueryBuilder Using(string collection)
		{
			Collection = collection;

			return this;
		}

		public async Task<IQueryable<dynamic>> Execute(string sql)
		{
			SqlStatement = sql;
			return await Repository.Execute(this);
		}
	}
}
