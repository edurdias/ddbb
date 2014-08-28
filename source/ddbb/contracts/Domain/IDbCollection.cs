using System.Collections.Generic;

namespace ddbb.App.Contracts.Domain
{
	public interface IDbCollection
	{
		string Name { get; set; }

		IEnumerable<IDbSproc> StoredProcedures { get; set; } 

		IEnumerable<IDbTrigger> Triggers { get; set; } 

		IEnumerable<IDbFunction> Functions { get; set; } 
	}
}