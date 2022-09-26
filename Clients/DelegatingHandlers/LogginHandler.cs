using System.Diagnostics;

namespace HttpClients_and_Polly.Clients.DynamicHandlers
{
    public class LoggingHandler: DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Starting request");

            var response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine($"Request finished in {sw.Elapsed}");

            return response;
        }
    }
}
