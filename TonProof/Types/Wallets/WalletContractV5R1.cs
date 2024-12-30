using TonLibDotNet.Cells;

namespace TonProof.Types.Wallets;

internal sealed class WalletContractV5R1 : IWalletContract
{
    public const string CodeBase64 =
        "te6cckECFAEAAoEAART/APSkE/S88sgLAQIBIAIDAgFIBAUBAvIOAtzQINdJwSCRW49jINcLHyCCEGV4dG69IYIQc2ludL2wkl8D4IIQZXh0brqOtIAg1yEB0HTXIfpAMPpE+Cj6RDBYvZFb4O1E0IEBQdch9AWDB/QOb6ExkTDhgEDXIXB/2zzgMSDXSYECgLmRMOBw4hAPAgEgBgcCASAICQAZvl8PaiaECAoOuQ+gLAIBbgoLAgFIDA0AGa3OdqJoQCDrkOuF/8AAGa8d9qJoQBDrkOuFj8AAF7Ml+1E0HHXIdcLH4AARsmL7UTQ1woAgAR4g1wsfghBzaWduuvLgin8PAeaO8O2i7fshgwjXIgKDCNcjIIAg1yHTH9Mf0x/tRNDSANMfINMf0//XCgAK+QFAzPkQmiiUXwrbMeHywIffArNQB7Dy0IRRJbry4IVQNrry4Ib4I7vy0IgikvgA3gGkf8jKAMsfAc8Wye1UIJL4D95w2zzYEAP27aLt+wL0BCFukmwhjkwCIdc5MHCUIccAs44tAdcoIHYeQ2wg10nACPLgkyDXSsAC8uCTINcdBscSwgBSMLDy0InXTNc5MAGk6GwShAe78uCT10rAAPLgk+1V4tIAAcAAkVvg69csCBQgkXCWAdcsCBwS4lIQseMPINdKERITAJYB+kAB+kT4KPpEMFi68uCR7UTQgQFB1xj0BQSdf8jKAEAEgwf0U/Lgi44UA4MH9Fvy4Iwi1woAIW4Bs7Dy0JDiyFADzxYS9ADJ7VQAcjDXLAgkji0h8uCS0gDtRNDSAFETuvLQj1RQMJExnAGBAUDXIdcKAPLgjuLIygBYzxbJ7VST8sCN4gAQk1vbMeHXTNCon9ZI";

    public static WalletContractV5R1 Create() => new();

    public string LoadPublicKey(Cell data)
    {
        Slice slice = data.BeginRead();
        slice.SkipBits(1);
        slice.SkipBits(32);
        slice.SkipBits(32);
        byte[] publicKeyBytes = slice.LoadBytes(32);
        return Convert.ToHexString(publicKeyBytes);
    }
}