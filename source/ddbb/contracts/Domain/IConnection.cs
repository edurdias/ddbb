using System.Collections.Generic;

namespace ddbb.App.Contracts.Domain
{
	public interface IConnection
	{
		long Id { get; set; }

		string Name { get; set; }

		string EndpointUrl { get; set; }

		string AuthorizationKey { get; set; }

		IEnumerable<IDatabase> Databases { get; set; }
	}
}