using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Events
{
	public interface ICollectionOpenedEvent
	{
		IDbCollection Collection { get; set; }
	}
}