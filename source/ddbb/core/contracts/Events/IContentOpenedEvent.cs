using System;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.Events
{
	public interface IContentOpenedEvent
	{
		Type Type { get; set; }

		object Content { get; set; }
	}
}