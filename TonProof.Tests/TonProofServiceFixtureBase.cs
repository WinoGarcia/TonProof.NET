using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TonLibDotNet;
using TonLibDotNet.Types;

namespace TonProof.Tests;

public abstract class TonProofServiceFixtureBase : IDisposable
***REMOVED***
    private readonly TonProofService tonProofService;
    private readonly TonClient tonClient;

    protected TonProofServiceFixtureBase(bool useMainnet)
    ***REMOVED***
        var tonClientLogger = new Mock<ILogger<TonClient>>(MockBehavior.Loose);
        var tonClientOptions = new Mock<IOptions<TonOptions>>(MockBehavior.Strict);
        tonClientOptions
            .SetupGet(x => x.Value)
            .Returns(() =>
            ***REMOVED***
                TonOptions o = new()
                ***REMOVED***
                    UseMainnet = useMainnet,
                    Options =
                    ***REMOVED***
                        KeystoreType = new KeyStoreTypeInMemory()
                ***REMOVED***
            ***REMOVED***;
                return o;
        ***REMOVED***);

        this.tonClient = new TonClient(tonClientLogger.Object, tonClientOptions.Object);

        var proofCheckLogger = new Mock<ILogger<TonProofService>>(MockBehavior.Loose);
        var proofCheckOptions = new Mock<IOptions<TonProofOptions>>(MockBehavior.Strict);
        proofCheckOptions
            .SetupGet(x => x.Value)
            .Returns(() =>
            ***REMOVED***
                var o = new TonProofOptions()
                ***REMOVED***
                    ValidAuthTime = 60 * 60 * 24 * 365 * 30, //~30 years
                    AllowedDomains = ["winogarcia.github.io"]
            ***REMOVED***;
                return o;
        ***REMOVED***);
        this.tonProofService = new TonProofService(proofCheckLogger.Object, this.tonClient, proofCheckOptions.Object);
***REMOVED***

    public ITonProofService GetProofCheckService() => this.tonProofService;

    public void Dispose()
    ***REMOVED***
        this.Dispose(true);
        GC.SuppressFinalize(this);
***REMOVED***

    protected virtual void Dispose(bool disposing)
    ***REMOVED***
        this.tonClient.Dispose();
***REMOVED***
***REMOVED***