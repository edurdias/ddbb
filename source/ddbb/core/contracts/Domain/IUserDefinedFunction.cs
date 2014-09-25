namespace ddbb.App.Contracts.Domain
{
	public interface IUserDefinedFunction : IDbScript
	{
		string SelfLink { get; set; }
	}
}