using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ddbb.Data
{
    public interface IConnection
    {
		string Url { get; set; }

		string Key { get; set; }

		string Database { get; set; }
    }

	public class DocumentDbConnection : IConnection
	{
		public string Url { get; set; }

		public string Key { get; set; }

		public string Database { get; set; }
	}
}
