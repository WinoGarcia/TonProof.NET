using Microsoft.Extensions.DependencyInjection;

namespace TonProof.Tests.Fixtures;

public static class HttpClientFactory
{
    #region Public Methods

    public static HttpClient CreateHttpClient<T>()
        where T : class
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddHttpClient<T>("ResilientClient")
            .AddStandardResilienceHandler();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();

        return factory.CreateClient("ResilientClient");
    }

    #endregion
}