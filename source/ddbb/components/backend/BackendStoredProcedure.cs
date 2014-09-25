using ddbb.App.Contracts.Domain;
using Microsoft.Azure.Documents;

namespace ddbb.Components.Backend
{
	public class BackendStoredProcedure : IStoredProcedure
	{
		public BackendStoredProcedure(StoredProcedure storedProcedure)
		{
			SelfLink = storedProcedure.SelfLink;
			Body = storedProcedure.Body;
		}

		public string SelfLink { get; set; }

		public string Body { get; set; }
	}
}