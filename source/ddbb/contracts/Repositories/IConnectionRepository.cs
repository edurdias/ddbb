using System.Collections.Generic;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Repositories
{
	public interface IConnectionRepository
	{
		IEnumerable<IConnection> All();
		void Add(List<IConnection> items);
		void Remove(List<IConnection> items);
		void Update(Dictionary<IConnection, IConnection> items);
	}
}