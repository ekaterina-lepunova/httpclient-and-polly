using Polly;
using Polly.Extensions.Http;
using HttpClients_and_Polly.Clients;
using HttpClients_and_Polly.Clients.DynamicHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Ex 1: custom DelegatingHandler
builder.Services.AddTransient<LoggingHandler>();

// Ex 2: 3rd party DelegatingHandler from Polly lib
builder.Services
    .AddHttpClient("google", _ =>   // named HttpClient
    {
        _.BaseAddress = new Uri("http://google.com");
    })
    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));

// Ex 3: how to reuse policy
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));

builder.Services
    .AddHttpClient<ISteavegordonPageClient, StevegordonPageClient>()    // typed HttpClient
    .AddHttpMessageHandler<LoggingHandler>()
    .AddPolicyHandler(timeoutPolicy)
    .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));

// Ex 4: how to use different policies based on HttmMethod type
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .RetryAsync(3);
var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

builder.Services
    .AddHttpClient<ITypicodeClient, TypicodeClient>()   // typed HttpClient
    .AddPolicyHandler(timeoutPolicy)
    .AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOpPolicy);

// Ex 5: how to use policy registry
var policyRegistry = builder.Services.AddPolicyRegistry();
policyRegistry.Add("timeout", timeoutPolicy);
policyRegistry.Add("retry", retryPolicy);
policyRegistry.Add("noOp", noOpPolicy);

builder.Services
    .AddHttpClient("yandex", _ =>   // named HttpClient
    {
        _.BaseAddress = new Uri("http://ya.ru");
    })
    .AddPolicyHandlerFromRegistry("timeout");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
