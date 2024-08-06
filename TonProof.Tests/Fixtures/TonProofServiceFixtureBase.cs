using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TonLibDotNet;
using TonLibDotNet.Types;

namespace TonProof.Tests.Fixtures;

public abstract class TonProofServiceFixtureBase : IDisposable
{
    #region Private Fields

    private readonly TonProofService tonProofService;
    private readonly TonClient tonClient;
    private readonly HttpClient httpClient;
    private readonly TonLibPublicKeyProvider tonLibProvider;
    private readonly TonApiPublicKeyProvider tonApiProvider;

    #endregion

    #region Constructors

    protected TonProofServiceFixtureBase(bool useMainnet, bool clearKnownWallet)
    {
        this.tonClient = this.InitTonClient(useMainnet);

        this.tonProofService = this.InitTonProofService(clearKnownWallet);

        this.tonLibProvider = new TonLibPublicKeyProvider(this.tonClient);
        this.httpClient = HttpClientFactory.CreateHttpClient<IPublicKeyProvider>();

        this.httpClient.BaseAddress = useMainnet
            ? new Uri("https://tonapi.io")
            : new Uri("https://testnet.tonapi.io");

        this.tonApiProvider = new TonApiPublicKeyProvider(this.httpClient);
    }

    #endregion

    #region Public Methods

    public IPublicKeyProvider GetTonApiProviderAsync() => this.tonApiProvider;

    public async Task<ITonProofService> GetProofCheckServiceAsync()
    {
        await this.tonClient.InitIfNeeded();
        await this.tonClient.Sync();
        return this.tonProofService;
    }

    public async Task<IPublicKeyProvider> GetTonLibProviderAsync()
    {
        await this.tonClient.InitIfNeeded();
        await this.tonClient.Sync();

        return this.tonLibProvider;
    }

    #endregion

    #region Private Methods

    private TonProofService InitTonProofService(bool clearKnownWallet)
    {
        var proofCheckLogger = new Mock<ILogger<TonProofService>>(MockBehavior.Loose);
        var proofCheckOptions = new Mock<IOptions<TonProofOptions>>(MockBehavior.Strict);
        proofCheckOptions
            .SetupGet(x => x.Value)
            .Returns(() =>
            {
                var o = new TonProofOptions()
                {
                    ValidAuthTime = 60 * 60 * 24 * 365 * 30, //~30 years
                    AllowedDomains = ["winogarcia.github.io"]
                };
                if (clearKnownWallet)
                {
                    o.KnownWallets.Clear();
                }

                return o;
            });
        var publicKeyProvider = new TonLibPublicKeyProvider(this.tonClient);
        return new TonProofService(proofCheckLogger.Object, this.tonClient, publicKeyProvider, proofCheckOptions.Object);
    }

    private TonClient InitTonClient(bool useMainnet)
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

        return new TonClient(tonClientLogger.Object, tonClientOptions.Object);
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        this.tonClient.Dispose();
        this.httpClient.Dispose();
    }

    #endregion
}