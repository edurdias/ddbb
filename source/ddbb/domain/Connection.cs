using System.ComponentModel.Composition;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Domain
{
	[Export(typeof(IConnection))]
	public class Connection : IConnection
	{
		public string Name { get; set; }

		public string EndpointUrl { get; set; }

		public string AuthorizationKey { get; set; }
	}
}