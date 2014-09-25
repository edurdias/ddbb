namespace ddbb.App.Contracts.Domain
{
	public interface IStoredProcedure : IDbScript
	{
		string SelfLink { get; set; }
	}
}