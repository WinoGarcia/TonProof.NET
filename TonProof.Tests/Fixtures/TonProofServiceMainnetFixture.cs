namespace TonProof.Tests.Fixtures;

public class TonProofServiceMainnetFixture() : TonProofServiceFixtureBase(true, false);

public class PublicKeyProviderMainnetFixture() : TonProofServiceFixtureBase(true, true);

public class TonProofServiceTestnetFixture() : TonProofServiceFixtureBase(false, false);

public class  PublicKeyProviderTestnetFixture() : TonProofServiceFixtureBase(false, true);
