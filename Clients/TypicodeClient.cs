namespace HttpClients_and_Polly.Clients
{
    public class TypicodeClient : ITypicodeClient
    {
        public HttpClient Client { get; }

        public TypicodeClient(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        }

        public async Task<string> GetDummyJson(string relativeUri)
        {
            var response = await Client.GetAsync(relativeUri);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
