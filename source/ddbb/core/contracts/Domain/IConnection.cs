using System.Collections.Generic;
using ddbb.App.Contracts.Services;

namespace ddbb.App.Contracts.Domain
{
	public interface IConnection
	{
		long Id { get; set; }

		string Name { get; set; }

		string EndpointUrl { get; set; }

		string AuthorizationKey { get; set; }

		IEnumerable<IDatabase> Databases { get; set; }
		
		IDatabase CreateDatabase(string name);
	}
}