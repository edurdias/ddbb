using ddbb.App.Contracts.Domain;
using Microsoft.Azure.Documents;

namespace ddbb.Components.Backend
{
	public class BackendUserDefinedFunction : IUserDefinedFunction
	{
		public BackendUserDefinedFunction(UserDefinedFunction udf)
		{
			SelfLink = udf.SelfLink;
			Body = udf.Body;
		}

		public string SelfLink { get; set; }

		public string Body { get; set; }
	}
}