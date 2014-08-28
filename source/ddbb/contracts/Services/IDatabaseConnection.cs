using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Services
{
	public interface IDatabaseConnection : IConnection
	{
		string Database { get; set; }
	}
}
