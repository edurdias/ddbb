using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Domain
{
	[Export(typeof(IConnection))]
	public class Connection : IConnection
	{
		[Key]
		public Int64 Id { get; set; }

		public string Name { get; set; }

		public string EndpointUrl { get; set; }

		public string AuthorizationKey { get; set; }

		[NotMapped]
		public IEnumerable<IDatabase> Databases { get; set; }
	}
}