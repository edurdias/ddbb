using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Events
{
	public interface IConnectionInProgressEvent
	{
		IConnection Connection { get; set; }
	}
}