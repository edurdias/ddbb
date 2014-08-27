namespace ddbb.App.Contracts.Domain
{
	public interface IConnection
	{
		string Name { get; set; }
		string EndpointUrl { get; set; }
		string AuthorizationKey { get; set; }
	}
}