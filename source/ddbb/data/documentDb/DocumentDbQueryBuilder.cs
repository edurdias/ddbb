using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using ddbb.App.Data.DataContracts;

namespace ddbb.App.Data.DocumentDb
{
	[Export(typeof(IQueryBuilder))]
	public class DocumentDbQueryBuilder : IQueryBuilder
	{
		private string _collection;
		private string _sqlStatement;

		public DocumentDbQueryBuilder(IQueryExecutor executor, IConnection connection)
		{
			Executor = executor;
			Connection = connection;
		}

		protected IQueryExecutor Executor { get; set; }

		protected IConnection Connection { get; private set; }

		string IQueryBuilder.Collection
		{
			get
			{
				return _collection;
			}
		}

		string IQueryBuilder.SqlStatement
		{
			get
			{
				return _sqlStatement;
			}
		}

		IConnection IQueryBuilder.GetConnection()
		{
			return Connection;
		}

		public IQueryBuilder Using(string collection)
		{
			_collection = collection;

			return this;
		}

		public async Task<IQueryable<dynamic>> Execute(string sql)
		{
			_sqlStatement = sql;
			return await Executor.Execute(this);
		}
	}
}
