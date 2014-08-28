using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Repositories;
using ddbb.App.Domain;

namespace ddbb.App.Infrastructure.Repositories
{
	[Export(typeof(IConnectionRepository))]
	public class ConnectionRepository : IConnectionRepository
	{
		[ImportingConstructor]
		public ConnectionRepository()
		{
			Context = new SqLiteDbContext();
		}

		private SqLiteDbContext Context { get; set; }

		public IEnumerable<IConnection> All()
		{
			return Context.Connections;
		}

		public void Add(IEnumerable<IConnection> items)
		{
			Context.Connections.AddRange(items.OfType<Connection>());
			Context.SaveChanges();
		}

		public void Remove(IEnumerable<IConnection> items)
		{
			Context.Connections.RemoveRange(items.OfType<Connection>());
			Context.SaveChanges();
		}

		public void Update(IEnumerable<IConnection> items)
		{
			foreach (var connection in items)
				Context.Connections.Attach((Connection) connection);
			Context.SaveChanges();
		}
	}

}