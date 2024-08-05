namespace TonProof.Tests.Fixtures;

[CollectionDefinition(Definition)]
public class TonProofServiceMainnetCollection : ICollectionFixture<TonProofServiceMainnetFixture>
{
    public const string Definition = "mainnet";
}

[CollectionDefinition(Definition)]
public class PublicKeyMainnetCollection : ICollectionFixture<PublicKeyProviderMainnetFixture>
{
    public const string Definition = "publicKeyProviderMainnet";
}

[CollectionDefinition(Definition)]
public class TonProofServiceTestnetCollection : ICollectionFixture<TonProofServiceTestnetFixture>
{
    public const string Definition = "testnet";
}

[CollectionDefinition(Definition)]
public class PublicKeyTestnetCollection : ICollectionFixture<PublicKeyProviderTestnetFixture>
{
    public const string Definition = "publicKeyProviderTestnet";
}