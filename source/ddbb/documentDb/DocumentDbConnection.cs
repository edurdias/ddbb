using ddbb.App.Contracts.Services;

namespace ddbb.App.Services.DocumentDb
{
	public class DocumentDbConnection : IDatabaseConnection
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string EndpointUrl { get; set; }

		public string AuthorizationKey { get; set; }

		public string Database { get; set; }
	}
}
