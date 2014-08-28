using System;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Reflection;

namespace ddbb.App.Infrastructure.Repositories
{
	public class SqLiteDbConfiguration : DbConfiguration
	{
		public SqLiteDbConfiguration()
		{
			SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
			SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
			SetDefaultConnectionFactory(new SqlConnectionFactory());

			// ProviderServices for SQLite is not public http://stackoverflow.com/a/23237737
			var t = Type.GetType("System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6");
			if (t == null) return;
			
			var field = t.GetField("Instance", BindingFlags.NonPublic | BindingFlags.Static);
			
			if (field == null) return;
			SetProviderServices("System.Data.SQLite", (DbProviderServices) field.GetValue(null));
		}
	}
}