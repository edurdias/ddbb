using System.Collections.Generic;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Repositories
{
	public interface IConnectionRepository
	{
		IEnumerable<IConnection> All();

		void Add(IEnumerable<IConnection> items);

		void Remove(IEnumerable<IConnection> items);

		void Update(IEnumerable<IConnection> items);
	}
}