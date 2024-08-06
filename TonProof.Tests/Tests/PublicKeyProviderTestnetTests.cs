using TonProof.Tests.Fixtures;

namespace TonProof.Tests.Tests;

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
    
    [Theory]
    [InlineData("0:7afd7563d8fd084d35060278320769743905983b8ea43c5b16fb6e3dc0a190dd", "e635249f6b3fbaf9742ae1d09d8f6f01b6082ff9256d719006329405019f3198")]
    public async Task TonApiPublicKeysNotEqual(string address, string publicKey)
    {
        var publicKeyProvider = this.getTonApiProvider();

        var result = await publicKeyProvider.GetPublicKeyAsync(address, this.cts.Token);

        Assert.NotEqual(result, publicKey);
    }

    [Theory]
    [InlineData("0:7afd7563d8fd084d35060278320769743905983b8ea43c5b16fb6e3dc0a190dd", "e635249f6b3fbaf9742ae1d09d8f6f01b6082ff9256d719006329405019f3198")]
    public async Task TonLibPublicKeysNotEqual(string address, string publicKey)
    {
        var publicKeyProvider = await this.getTonLibProvider();

        var result = await publicKeyProvider.GetPublicKeyAsync(address, this.cts.Token);

        Assert.NotEqual(result, publicKey);
    }

    #endregion
}