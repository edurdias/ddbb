using System.Collections.Generic;
using System.ComponentModel.Composition;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Repositories;

namespace ddbb.App.Infrastructure.Repositories
{
	[Export(typeof(IConnectionRepository))]
	public class ConnectionRepository : IConnectionRepository
	{
		public IEnumerable<IConnection> All()
		{
			return new List<IConnection>();
		}

		public void Add(List<IConnection> items)
		{
			throw new System.NotImplementedException();
		}

		public void Remove(List<IConnection> items)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Dictionary<IConnection, IConnection> items)
		{
			throw new System.NotImplementedException();
		}
	}

}