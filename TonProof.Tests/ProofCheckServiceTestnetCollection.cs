namespace TonProof.Tests;

[CollectionDefinition(Definition)]
public class ProofCheckServiceTestnetCollection : ICollectionFixture<TonProofServiceTestnetFixture>
{
    public const string Definition = "testnet";
}