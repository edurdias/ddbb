using System.Data.Entity;
using System.Data.SQLite;
using ddbb.App.Domain;

namespace ddbb.App.Infrastructure.Repositories
{
	[DbConfigurationType(typeof(SqLiteDbConfiguration))]
	public class SqLiteDbContext : DbContext
	{
		public SqLiteDbContext()
			: base(new SQLiteConnection("Data Source=ddbb.db"), true)
		{
		}

		public DbSet<Connection> Connections { get; set; }

	}
}
