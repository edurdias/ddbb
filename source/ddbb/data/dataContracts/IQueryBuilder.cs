﻿using System.Linq;
using System.Threading.Tasks;

namespace ddbb.App.Data.DataContracts
{
	public interface IQueryBuilder
	{
		string GetCollection();

		string GetSqlStatement();

		IDatabaseConnection GetDatabaseConnection();

		IQueryBuilder Using(string collection);

		Task<IQueryable<dynamic>> Execute(string sql);
	}
}
