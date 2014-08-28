using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Services
{
	public interface IBackendService
	{
		bool IsValid(string endpointUrl, string authenticationKey);
		
		IConnection Connect(IConnection connection);
	}
}