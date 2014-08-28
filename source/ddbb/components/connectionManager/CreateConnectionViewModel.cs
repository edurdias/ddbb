using System.Windows;
using Caliburn.Micro;

namespace ddbb.App.Components.ConnectionManager
{
	public class CreateConnectionViewModel : Screen
	{
		public CreateConnectionViewModel()
		{
			
		}

		public string Name { get; set; }

		public string EndpointUrl { get; set; }

		public string AuthenticationKey { get; set; }

		public bool CanTestConnection
		{
			get { return IsValid(); }
		}

		public bool CanSave
		{
			get { return IsValid(); }
		}

		public void TestConnection()
		{
			
		}

		public void Save()
		{
			
		}

		public void Cancel()
		{
			
		}


		private bool IsValid()
		{
			return !string.IsNullOrEmpty(Name)
			       && !string.IsNullOrEmpty(EndpointUrl)
			       && !string.IsNullOrEmpty(AuthenticationKey);
		}
	}
}