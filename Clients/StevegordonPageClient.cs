namespace HttpClients_and_Polly.Clients
{
    public class StevegordonPageClient : ISteavegordonPageClient
    {
        public HttpClient Client { get; }

        public StevegordonPageClient(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("https://www.stevejgordon.co.uk/");
        }

        public async Task<string> GetContent(string relativeUri)
        {
            if (Client?.BaseAddress is null)
            {
                return string.Empty;
            }

            var url = new Uri(Client.BaseAddress, relativeUri);
            var response = await Client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
