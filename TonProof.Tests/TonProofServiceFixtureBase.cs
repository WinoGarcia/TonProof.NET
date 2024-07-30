using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TonLibDotNet;
using TonLibDotNet.Types;

namespace TonProof.Tests;

public abstract class TonProofServiceFixtureBase : IDisposable
{
    private readonly TonProofService tonProofService;
    private readonly TonClient tonClient;

    protected TonProofServiceFixtureBase(bool useMainnet)
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
                    Options =
                    {
                        KeystoreType = new KeyStoreTypeInMemory()
                    }
                };
                return o;
            });

        this.tonClient = new TonClient(tonClientLogger.Object, tonClientOptions.Object);

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
                return o;
            });
        this.tonProofService = new TonProofService(proofCheckLogger.Object, this.tonClient, proofCheckOptions.Object);
    }

    public ITonProofService GetProofCheckService() => this.tonProofService;

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        this.tonClient.Dispose();
    }
}