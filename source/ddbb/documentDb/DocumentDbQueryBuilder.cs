using System.ComponentModel.Composition;
using System.Linq;
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

		public DocumentDbQueryBuilder(IDocumentDbService service, IConnection connection)
		{
			Service = service;
			Connection = connection;
		}

		protected IDocumentDbService Service { get; set; }

		protected IConnection Connection { get; private set; }

		string IQueryBuilder.GetCollection()
		{
			return _collection;
		}

		string IQueryBuilder.GetSqlStatement()
		{
			return _sqlStatement;
		}

		IDatabaseConnection IQueryBuilder.GetDatabaseConnection()
		{
			return (IDatabaseConnection)Connection;
		}

		public IQueryBuilder Using(string collection)
		{
			_collection = collection;

			return this;
		}

		public async Task<IQueryable<dynamic>> Execute(string sql)
		{
			_sqlStatement = sql;
			return await Service.Execute(this);
		}
	}
}
