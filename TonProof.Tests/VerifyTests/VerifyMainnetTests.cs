using TonProof.Tests.Data;
using TonProof.Tests.Fixtures;
using TonProof.Types;

namespace TonProof.Tests.VerifyTests;

[Collection(TonProofServiceMainnetCollection.Definition)]
public class VerifyMainnetTests(TonProofServiceMainnetFixture fixtureBase)
{
    #region Private Fields

    private readonly Func<Task<ITonProofService>> getProofCheckService = fixtureBase.GetProofCheckServiceAsync;
    private readonly CancellationTokenSource cts = new();

    #endregion

    #region Tests

    [Theory]
    [ClassData(typeof(CheckProofRequestV3R1Mainnet))]
    [ClassData(typeof(CheckProofRequestV4R2Mainnet))]
    public async Task Valid(CheckProofRequest request)
    {
        var proofCheckService = await this.getProofCheckService();

        var result = await proofCheckService.VerifyAsync(request, this.cts.Token);

        Assert.Equal(VerifyResult.Valid, result);
    }

    #endregion
}