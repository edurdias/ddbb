using System.Collections.Generic;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Services.DocumentDb.Models
{
	public class DocumentDbCollection : IDbCollection
	{
		public string Name { get; set; }

		public IEnumerable<IDbSproc> StoredProcedures { get; set; }

		public IEnumerable<IDbTrigger> Triggers { get; set; }

		public IEnumerable<IDbFunction> Functions { get; set; }
	}
}