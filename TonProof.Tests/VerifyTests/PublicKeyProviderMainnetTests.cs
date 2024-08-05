using TonProof.Tests.Fixtures;

namespace TonProof.Tests.VerifyTests;

[Collection(PublicKeyMainnetCollection.Definition)]
public class PublicKeyProviderMainnetTests(PublicKeyProviderMainnetFixture fixtureBase)
{
    #region Private Fields

    private readonly Func<IPublicKeyProvider> getTonApiProvider = fixtureBase.GetTonApiProviderAsync;
    private readonly Func<Task<IPublicKeyProvider>> getTonLibProvider = fixtureBase.GetTonLibProviderAsync;
    private readonly CancellationTokenSource cts = new();

    #endregion

    #region Tests

    [Theory]
    [InlineData("0:c83546fdc5fac87d55f04804919c0829bc0e6be24b8e564590dbc44380c30ee9", "e635249f6b3fbaf9742ae1d09d8f6f01b6082ff9256d719006329405019f3198")]
    public async Task TonApiPublicKeysEqual(string address, string publicKey)
    {
        var publicKeyProvider = this.getTonApiProvider();

        var result = await publicKeyProvider.GetPublicKeyAsync(address, this.cts.Token);

        Assert.Equal(result, publicKey);
    }
    
    [Theory]
    [InlineData("0:c83546fdc5fac87d55f04804919c0829bc0e6be24b8e564590dbc44380c30ee9", "e635249f6b3fbaf9742ae1d09d8f6f01b6082ff9256d719006329405019f3198")]
    public async Task TonLibPublicKeysEqual(string address, string publicKey)
    {
        var publicKeyProvider = await this.getTonLibProvider();

        var result = await publicKeyProvider.GetPublicKeyAsync(address, this.cts.Token);

        Assert.Equal(result, publicKey);
    }

    #endregion
}