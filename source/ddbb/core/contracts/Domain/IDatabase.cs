using System.Collections.Generic;

namespace ddbb.App.Contracts.Domain
{
	public interface IDatabase
	{
		string Name { get; set; }

		IConnection ParentConnection { get; set; }

		IEnumerable<IDbCollection> Collections { get; set; }

		IEnumerable<IDbUser> Users { get; set; } 
	}
}