using TonProof.Tests.Fixtures;

namespace TonProof.Tests.VerifyTests;

[Collection(PublicKeyTestnetCollection.Definition)]
public class PublicKeyProviderTestnetTests(PublicKeyProviderTestnetFixture fixtureBase)
{
    #region Private Fields

    private readonly Func<IPublicKeyProvider> getTonApiProvider = fixtureBase.GetTonApiProviderAsync;
    private readonly Func<Task<IPublicKeyProvider>> getTonLibProvider = fixtureBase.GetTonLibProviderAsync;
    private readonly CancellationTokenSource cts = new();

    #endregion

    #region Tests

    [Theory]
    [InlineData("0:13f04fa2a978c6eaccc5c0521c44d87b5be7f673ff20176a0203d90e8b90a7c1", "c5134fcb39879c2cf81ffdc347353d031cf184538130fdd42688152088bf69ba")]
    public async Task TonApiPublicKeysEqual(string address, string publicKey)
    {
        var publicKeyProvider = this.getTonApiProvider();

        var result = await publicKeyProvider.GetPublicKeyAsync(address, this.cts.Token);

        Assert.Equal(result, publicKey);
    }

    [Theory]
    [InlineData("0:13f04fa2a978c6eaccc5c0521c44d87b5be7f673ff20176a0203d90e8b90a7c1", "c5134fcb39879c2cf81ffdc347353d031cf184538130fdd42688152088bf69ba")]
    public async Task TonLibPublicKeysEqual(string address, string publicKey)
    {
        var publicKeyProvider = await this.getTonLibProvider();

        var result = await publicKeyProvider.GetPublicKeyAsync(address, this.cts.Token);

        Assert.Equal(result, publicKey);
    }

    #endregion
}