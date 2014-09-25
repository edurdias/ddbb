using ddbb.App.Contracts.Domain;
using Microsoft.Azure.Documents;

namespace ddbb.Components.Backend
{
	public class BackendTrigger : ITrigger
	{
		public BackendTrigger(Trigger trigger)
		{
			SelfLink = trigger.SelfLink;
			Body = trigger.Body;
		}

		public string SelfLink { get; set; }

		public string Body { get; set; }
	}
}