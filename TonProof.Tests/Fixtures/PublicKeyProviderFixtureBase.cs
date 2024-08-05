using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TonLibDotNet;
using TonLibDotNet.Types;

namespace TonProof.Tests.Fixtures;

public class PublicKeyProviderFixtureBase : IDisposable
{
    #region Private Fields

    private readonly TonClient tonClient;
    private readonly HttpClient httpClient;
    private readonly TonLibPublicKeyProvider tonLibProvider;
    private readonly TonApiPublicKeyProvider providerTestnetTestnet;

    #endregion

    #region Constructors

    public PublicKeyProviderFixtureBase(bool useMainnet)
    {
        var tonClientLogger = new Mock<ILogger<TonClient>>(MockBehavior.Loose);
        var tonClientOptions = new Mock<IOptions<TonOptions>>(MockBehavior.Strict);
        tonClientOptions
            .SetupGet(x => x.Value)
            .Returns(() =>
            {
                TonOptions o = new()
                {
                    UseMainnet = useMainnet,
                    LogTextLimit = 0,
                    Options =
                    {
                        KeystoreType = new KeyStoreTypeDirectory("D:/Temp/keys") //new KeyStoreTypeInMemory() 
                    }
                };
                return o;
            });

        this.tonClient = new TonClient(tonClientLogger.Object, tonClientOptions.Object);
        this.tonLibProvider = new TonLibPublicKeyProvider(this.tonClient);
        
        this.httpClient = HttpClientFactory.CreateHttpClient<IPublicKeyProvider>();
        
        if (useMainnet)
        {
            this.httpClient.BaseAddress = new Uri("https://tonapi.io");
            //this.httpClient.DefaultRequestHeaders.Add("Authorization", "");
        }
        else
        {
            this.httpClient.BaseAddress = new Uri("https://testnet.tonapi.io");
            //this.httpClient.DefaultRequestHeaders.Add("Authorization", "");
        }
        var tonApiLogger = new Mock<ILogger<TonApiPublicKeyProvider>>(MockBehavior.Loose);
        this.providerTestnetTestnet = new TonApiPublicKeyProvider(tonApiLogger.Object, this.httpClient);
    }

    #endregion
    
    public IPublicKeyProvider GetTonApiProviderAsync() => this.providerTestnetTestnet;

    public async Task<IPublicKeyProvider> GetTonLibProviderAsync()
    {
        await this.tonClient.InitIfNeeded();
        await this.tonClient.Sync();

        return this.tonLibProvider;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        this.tonClient?.Dispose();
        this.httpClient?.Dispose();
    }
}