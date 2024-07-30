using TonLibDotNet;
using TonLibDotNet.Cells;
using InitialAccountState = TonLibDotNet.Types.Raw.InitialAccountState;

namespace TonProof.Types;

internal sealed class CheckProofRequestRaw : CheckProofRequest
{
    #region Constructors

    public CheckProofRequestRaw(CheckProofRequest request)
    {
        this.Address = request.Address;
        this.Network = request.Network;
        this.PublicKey = request.PublicKey;
        this.Proof = request.Proof;

        var boc = Boc.ParseFromBase64(request.Proof.StateInit);

        if (boc.RootCells[0].Refs.Count >= 2)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (boc.RootCells[0].Refs[0] is not null &&
                boc.RootCells[0].Refs[1] is not null)
            {
                this.Data = boc.RootCells[0].Refs[1];
                this.InitState = new InitialAccountState()
                {
                    Code = boc.RootCells[0].Refs[0].ToBoc().SerializeToBase64(),
                    Data = boc.RootCells[0].Refs[1].ToBoc().SerializeToBase64()
                };
            }
        }

        this.PublicKeyBytes = Convert.FromHexString(this.PublicKey);

        var addressSpan = this.Address.AsSpan();
        this.AddressBytes = Convert.FromHexString(addressSpan[2..]);

        if (!uint.TryParse(addressSpan[..1], out var wc))
        {
            throw new InvalidDataException("Invalid address format: The contract's address does not contain a workchain ID");
        }

        this.Workchain = wc;
    }

    #endregion

    #region Public Fields

    public byte[] AddressBytes { get; private set; }

    public uint Workchain { get; private set; }

    public byte[] PublicKeyBytes { get; private set; }

    public InitialAccountState InitState { get; private set; }

    public Cell Data { get; private set; }

    #endregion
}