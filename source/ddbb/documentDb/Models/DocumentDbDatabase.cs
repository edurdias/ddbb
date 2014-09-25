using System.Collections.Generic;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Services.DocumentDb.Models
{
	public class DocumentDbDatabase : IDatabase
	{
		public DocumentDbDatabase(IConnection connection)
		{
			Connection = connection;
		}

		public string Name { get; set; }

		public IConnection Connection { get; set; }

		public IEnumerable<IDbCollection> Collections { get; set; }

		public IEnumerable<IDbUser> Users { get; set; }
	}
}
