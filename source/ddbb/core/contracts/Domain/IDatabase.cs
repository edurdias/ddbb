using System.Collections.Generic;

namespace ddbb.App.Contracts.Domain
{
	public interface IDatabase
	{
		string Name { get; set; }

		IConnection Connection { get; set; }

		IEnumerable<IDocumentCollection> Collections { get; set; }

		IEnumerable<IDbUser> Users { get; set; }
		
		string SelfLink { get; set; }

		string CollectionsLink { get; set; }

		IDocumentCollection CreateCollection(string name);

		IDbUser CreateUser(string username);
	}
}