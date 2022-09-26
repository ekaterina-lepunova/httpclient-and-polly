namespace HttpClients_and_Polly.Clients
{
    public interface ITypicodeClient
    {
        Task<string> GetDummyJson(string relativeUri);
    }
}
