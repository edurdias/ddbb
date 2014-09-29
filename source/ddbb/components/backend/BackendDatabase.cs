using System;
using System.Collections.Generic;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;
using Microsoft.Azure.Documents;

namespace ddbb.Components.Backend
{
	public class BackendDatabase : IDatabase
	{
		private IEnumerable<IDocumentCollection> collections;

		public BackendDatabase(Database database)
		{
			Name = database.Id;
			SelfLink = database.SelfLink;
			CollectionsLink = database.CollectionsLink;
		}

		public string Name { get; set; }

		public IConnection Connection { get; set; }

		public IEnumerable<IDocumentCollection> Collections
		{
			get
			{
				if (collections == null)
					collections = IoC.Get<IBackend>().GetCollections(this);
				return collections;
			}
			set { collections = value; }
		}

		public IEnumerable<IDbUser> Users { get; set; }
		public string SelfLink { get; set; }

		public string CollectionsLink { get; set; }

		public Database Reference { get; set; }

		public IDocumentCollection CreateCollection(string name)
		{
			var backend = IoC.Get<IBackend>();
			return backend.CreateCollection(name, this);
		}

		public IDbUser CreateUser(string username)
		{
			throw new NotImplementedException();
		}
	}
}