using System.Collections.Generic;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Services.DocumentDb
{
	public class DocumentDbConnection : IConnection
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string EndpointUrl { get; set; }

		public string AuthorizationKey { get; set; }

		public IEnumerable<IDatabase> Databases { get; set; }
	}
}
