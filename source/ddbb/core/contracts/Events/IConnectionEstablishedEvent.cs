using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Events
{
	public interface IConnectionEstablishedEvent
	{
		IConnection Connection { get; }
	}
}