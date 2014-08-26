using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Ddbb.Data.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var connection = new DocumentDbConnection {
				Url = ConfigurationManager.AppSettings["SyngineDbUrl"],
				Key = ConfigurationManager.AppSettings["SyngineDbKey"],
				Database = ConfigurationManager.AppSettings["SyngineDbName"]
			};

			var repository = new Repository(connection);
			var collection = repository.Create("tests").Execute("select * from tests").Result;

			foreach (var item in collection) {
				
			}
		}
	}
}
