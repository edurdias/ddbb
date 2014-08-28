using System.Collections.Generic;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Repositories
{
	public interface IConnectionRepository
	{
		IEnumerable<IConnection> All();

		void Add(IConnection connection); 
		
		void Update(IConnection connection);

		void Remove(IConnection connection);
	}
}