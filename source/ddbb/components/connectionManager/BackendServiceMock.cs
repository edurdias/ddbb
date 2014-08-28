using System.ComponentModel.Composition;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;

namespace ddbb.App.Components.ConnectionManager
{
	[Export(typeof(IBackendService))]
	public class BackendServiceMock : IBackendService
	{
		public bool IsValid(string endpointUrl, string authenticationKey)
		{
			return true;
		}

		public IConnection Connect(IConnection connection)
		{
			return connection;
		}
	}
}