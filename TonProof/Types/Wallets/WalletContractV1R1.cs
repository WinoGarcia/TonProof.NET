using TonLibDotNet.Cells;

namespace TonProof.Types.Wallets;

internal class WalletContractV1R1 : IWalletContract
{
    public const string CodeBase64 = "te6cckEBAQEARAAAhP8AIN2k8mCBAgDXGCDXCx/tRNDTH9P/0VESuvKhIvkBVBBE+RDyovgAAdMfMSDXSpbTB9QC+wDe0aTIyx/L/8ntVEH98Ik=";

    public static WalletContractV1R1 Create() => new();
    
    public byte[] LoadPublicKey(Cell data)
    {
        var dataSlice = data.BeginRead();
        dataSlice.SkipBits(32); //seqno
        return dataSlice.LoadBytes(32); //publicKey
    }
}