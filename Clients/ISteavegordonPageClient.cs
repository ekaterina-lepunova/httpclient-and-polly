namespace HttpClients_and_Polly.Clients
{
    public interface ISteavegordonPageClient
    {
        Task<string> GetContent(string url);
    }
}
