using TonProof.Tests.Data;
using TonProof.Types;

namespace TonProof.Tests.VerifyTests;

[Collection(ProofCheckServiceTestnetCollection.Definition)]
public class VerifyTestnetTests(TonProofServiceTestnetFixture fixtureBase)
{
    #region Private Fields

    private readonly Func<ITonProofService> getProofCheckService = fixtureBase.GetProofCheckService;
    private readonly CancellationTokenSource cts = new();

    #endregion

    #region Tests

    [Theory]
    [ClassData(typeof(CheckProofRequestV4R2Testnet))]
    public async Task Valid(CheckProofRequest request)
    {
        var proofCheckService = this.getProofCheckService();

        var result = await proofCheckService.VerifyAsync(request, this.cts.Token);

        Assert.Equal(VerifyResult.Valid, result);
    }
    
    [Theory]
    [ClassData(typeof(CheckProofRequestV5Testnet))]
    public async Task InvalidInitState(CheckProofRequest request)
    {
        //TonConnect probably hasn't yet implemented support for walletV5.
        var proofCheckService = this.getProofCheckService();

        var result = await proofCheckService.VerifyAsync(request, this.cts.Token);

        Assert.Equal(VerifyResult.InvalidInitState, result);
    }

    [Theory]
    [ClassData(typeof(CheckProofRequestV4R2Testnet))]
    public async Task DomainNotAllowed(CheckProofRequest request)
    {
        var proofCheckService = this.getProofCheckService();

        request.Proof.Domain.Value = "ton.org";

        var result = await proofCheckService.VerifyAsync(request, this.cts.Token);

        Assert.Equal(VerifyResult.DomainNotAllowed, result);
    }

    [Theory]
    [ClassData(typeof(CheckProofRequestV4R2Testnet))]
    public async Task AddressMismatch(CheckProofRequest request)
    {
        var proofCheckService = this.getProofCheckService();

        request.Address = "0:7afd7563d8fd084d35060278320769743905983b8ea43c5b16fb6e3dc0a190dd";

        var result = await proofCheckService.VerifyAsync(request, this.cts.Token);

        Assert.Equal(VerifyResult.AddressMismatch, result);
    }

    [Theory]
    [ClassData(typeof(CheckProofRequestV4R2Testnet))]
    public async Task ProofExpired(CheckProofRequest request)
    {
        var proofCheckService = this.getProofCheckService();

        request.Proof.Timestamp = TimeSpan.MinValue.Seconds;

        var result = await proofCheckService.VerifyAsync(request, this.cts.Token);

        Assert.Equal(VerifyResult.ProofExpired, result);
    }

    [Theory]
    [ClassData(typeof(CheckProofRequestV4R2Testnet))]
    public async Task PublicKeyMismatch(CheckProofRequest request)
    {
        var proofCheckService = this.getProofCheckService();

        request.PublicKey = "e635249f6b3fbaf9742ae1d09d8f6f01b6082ff9256d719006329405019f3198";

        var result = await proofCheckService.VerifyAsync(request, this.cts.Token);

        Assert.Equal(VerifyResult.PublicKeyMismatch, result);
    }

    #endregion
}