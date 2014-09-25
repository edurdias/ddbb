using System.Collections.Generic;

namespace ddbb.App.Contracts.Domain
{
	public interface IDocumentCollection
	{
		string Name { get; set; }

		string SelfLink { get; set; }

		string DocumentsLink { get; set; }

		IEnumerable<IStoredProcedure> StoredProcedures { get; }

		IEnumerable<ITrigger> Triggers { get; }

		IEnumerable<IUserDefinedFunction> UserDefinedFunctions { get; }
		string StoredProceduresLink { get; set; }
		string TriggersLink { get; set; }
		string UserDefinedFunctionsLink { get; set; }
	}
}