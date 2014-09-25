namespace ddbb.App.Contracts.Domain
{
	public interface ITrigger : IDbScript
	{
		string SelfLink { get; set; }
	}
}