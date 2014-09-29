using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;

namespace ddbb.App.Domain
{
	[Export(typeof(IConnection))]
	public class Connection : IConnection
	{
		private IEnumerable<IDatabase> databases;

		[Key]
		public Int64 Id { get; set; }

		public string Name { get; set; }

		public string EndpointUrl { get; set; }

		public string AuthorizationKey { get; set; }

		[NotMapped]
		public IEnumerable<IDatabase> Databases
		{
			get
			{
				if (databases == null)
					databases = IoC.Get<IBackend>().GetDatabases(this);
				return databases;
			}
			set { databases = value; }
		}

		public IDatabase CreateDatabase(string name)
		{
			var backend = IoC.Get<IBackend>();
			return backend.CreateDatabase(name);
		}
	}
}