using TonLibDotNet.Cells;

namespace TonProof.Types.Wallets;

public interface IWalletContract
{
    string LoadPublicKey(Cell data);
}