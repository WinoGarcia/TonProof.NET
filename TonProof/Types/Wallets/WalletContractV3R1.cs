using TonLibDotNet.Cells;

namespace TonProof.Types.Wallets;

internal class WalletContractV3R1 : IWalletContract
{
    public const string CodeBase64 = "te6cckEBAQEAYgAAwP8AIN0gggFMl7qXMO1E0NcLH+Ck8mCDCNcYINMf0x/TH/gjE7vyY+1E0NMf0x/T/9FRMrryoVFEuvKiBPkBVBBV+RDyo/gAkyDXSpbTB9QC+wDo0QGkyMsfyx/L/8ntVD++buA=";

    public static WalletContractV3R1 Create() => new();
    
    public byte[] LoadPublicKey(Cell data)
    {
        var dataSlice = data.BeginRead();
        dataSlice.SkipBits(32); //seqno
        dataSlice.SkipBits(32); //walletId
        return dataSlice.LoadBytes(32); //publicKey
    }
}