using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Services;
using Microsoft.Azure.Documents;

namespace ddbb.Components.Backend
{
	public class BackendCollection : IDocumentCollection
	{
		private ReadOnlyCollection<IStoredProcedure> procedures;
		private ReadOnlyCollection<ITrigger> triggers;
		private ReadOnlyCollection<IUserDefinedFunction> functions;

		public BackendCollection(DocumentCollection collection)
		{
			Name = collection.Id;
			SelfLink = collection.SelfLink;
			DocumentsLink = collection.DocumentsLink;
			StoredProceduresLink = collection.StoredProceduresLink;
			TriggersLink = collection.TriggersLink;
			UserDefinedFunctionsLink = collection.UserDefinedFunctionsLink;
		}

		public string Name { get; set; }

		public string SelfLink { get; set; }

		public string DocumentsLink { get; set; }

		public string StoredProceduresLink { get; set; }

		public string TriggersLink { get; set; }

		public string UserDefinedFunctionsLink { get; set; }
		
		public IEnumerable<IStoredProcedure> StoredProcedures
		{
			get
			{
				if (procedures == null)
					procedures = new ReadOnlyCollection<IStoredProcedure>(IoC.Get<IBackend>().GetStoredProcedures(this));
				return procedures;
			}
		}

		public IEnumerable<ITrigger> Triggers
		{
			get
			{
				if (triggers == null)
					triggers = new ReadOnlyCollection<ITrigger>(IoC.Get<IBackend>().GetTriggers(this));
				return triggers;
			}
		}

		public IEnumerable<IUserDefinedFunction> UserDefinedFunctions
		{
			get
			{
				if (functions == null)
					functions = new ReadOnlyCollection<IUserDefinedFunction>(IoC.Get<IBackend>().GetUserDefinedFunctions(this));
				return functions;
			}
		}
	}
}