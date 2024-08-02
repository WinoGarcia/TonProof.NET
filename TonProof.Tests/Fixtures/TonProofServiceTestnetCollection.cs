using TonProof.Tests.Fixtures;

namespace TonProof.Tests;

[CollectionDefinition(Definition)]
public class TonProofServiceTestnetCollection : ICollectionFixture<TonProofServiceTestnetFixture>
{
    public const string Definition = "testnet";
}