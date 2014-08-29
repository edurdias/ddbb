
namespace ddbb.App.Data.DataContracts
{
	public interface IDatabaseConnection : IConnection
	{
		string Database { get; set; }
	}
}
