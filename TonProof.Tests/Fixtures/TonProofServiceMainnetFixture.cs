namespace TonProof.Tests.Fixtures;

public class TonProofServiceMainnetFixture() : TonProofServiceFixtureBase(true);

public class PublicKeyProviderMainnetFixture() : PublicKeyProviderFixtureBase(true);

public class TonProofServiceTestnetFixture() : TonProofServiceFixtureBase(false);

public class  PublicKeyProviderTestnetFixture() : PublicKeyProviderFixtureBase(false);
