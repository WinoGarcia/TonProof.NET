using TonLibDotNet.Cells;

namespace TonProof.Types.Wallets;

public interface IWalletContract
{
    byte[] LoadPublicKey(Cell data);
}