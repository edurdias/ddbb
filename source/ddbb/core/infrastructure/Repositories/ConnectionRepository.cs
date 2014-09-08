using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Repositories;
using ddbb.App.Domain;

namespace ddbb.App.Infrastructure.Repositories
{
	//TODO: review usage of concrete class Connection instead of its interface (contra-variance)
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

		public void Add(IConnection connection)
		{
			Context.Connections.Add((Connection)connection);
			Context.SaveChanges();
		}

		public void Update(IConnection connection)
		{
			Context.Entry((Connection)connection).State = EntityState.Modified;
			Context.SaveChanges();
		}

		public void Remove(IConnection connection)
		{
			Context.Connections.Remove((Connection)connection);
			Context.SaveChanges();
		}
	}

}