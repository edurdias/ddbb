using System;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Contracts.ViewModels
{
	public interface IContentView : IScreen
	{
		object Content { get; set; }
	}
}