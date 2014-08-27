using ddbb.App.Data.DataContracts;

namespace ddbb.App.Data.DocumentDb
{
	public class DocumentDbConnection : IConnection
	{
		public string Url { get; set; }

		public string Key { get; set; }

		public string Database { get; set; }
	}
}
