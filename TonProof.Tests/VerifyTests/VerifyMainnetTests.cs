using TonProof.Tests.Data;
using TonProof.Types;

namespace TonProof.Tests.VerifyTests;

[Collection(TonProofServiceMainnetCollection.Definition)]
public class VerifyMainnetTests(TonProofServiceMainnetFixture fixtureBase)
***REMOVED***
    #region Private Fields

    private readonly Func<ITonProofService> getProofCheckService = fixtureBase.GetProofCheckService;
    private readonly CancellationTokenSource cts = new();

    #endregion

    #region Tests

    [Theory]
    [ClassData(typeof(CheckProofRequestV3R1Mainnet))]
    [ClassData(typeof(CheckProofRequestV4R2Mainnet))]
    public async Task VerifyingIsValid(CheckProofRequest request)
    ***REMOVED***
        var proofCheckService = this.getProofCheckService();

        var result = await proofCheckService.VerifyAsync(request, this.cts.Token);

        Assert.Equal(VerifyResult.Valid, result);
***REMOVED***

    #endregion
***REMOVED***