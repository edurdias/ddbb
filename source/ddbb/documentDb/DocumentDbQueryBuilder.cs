using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;

namespace ddbb.App.Services.DocumentDb
{
	[Export(typeof(IQueryBuilder))]
	public class DocumentDbQueryBuilder : IQueryBuilder
	{
		private string _collection;
		private string _sqlStatement;

		public DocumentDbQueryBuilder(IDocumentDbService service, IDatabase database)
		{
			Service = service;
			Database = database;
		}

		protected IDocumentDbService Service { get; set; }

		protected IDatabase Database { get; set; }

		string IQueryBuilder.GetCollection()
		{
			return _collection;
		}

		string IQueryBuilder.GetSqlStatement()
		{
			return _sqlStatement;
		}

		IDatabase IQueryBuilder.GetDatabase()
		{
			return Database;
		}

		IConnection IQueryBuilder.GetConnection()
		{
			return Database.Connection;
		}

		public IQueryBuilder Using(string collection)
		{
			_collection = collection;

			return this;
		}

		public async Task<IEnumerable<dynamic>> Execute(string sql)
		{
			_sqlStatement = sql;
			return await Service.Execute(this);
		}
	}
}
