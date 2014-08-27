
namespace ddbb.App.Data.DataContracts
{
    public interface IConnection
    {
		string Url { get; set; }

		string Key { get; set; }

		string Database { get; set; }
    }
}
