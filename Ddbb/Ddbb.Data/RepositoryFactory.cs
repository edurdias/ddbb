using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ddbb.Data
{
	public class RepositoryFactory
	{
		public static IRepository Create(IConnection connection)
		{
			return new Repository(connection);
		}
	}
}
