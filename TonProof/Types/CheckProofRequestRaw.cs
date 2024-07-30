using TonLibDotNet;
using TonLibDotNet.Cells;
using InitialAccountState = TonLibDotNet.Types.Raw.InitialAccountState;

namespace TonProof.Types;

internal sealed class CheckProofRequestRaw : CheckProofRequest
***REMOVED***
    #region Constructors

    public CheckProofRequestRaw(CheckProofRequest request)
    ***REMOVED***
        this.Address = request.Address;
        this.Network = request.Network;
        this.PublicKey = request.PublicKey;
        this.Proof = request.Proof;

        var boc = Boc.ParseFromBase64(request.Proof.StateInit);

        if (boc.RootCells[0].Refs.Count >= 2)
        ***REMOVED***
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (boc.RootCells[0].Refs[0] is not null &&
                boc.RootCells[0].Refs[1] is not null)
            ***REMOVED***
                this.Data = boc.RootCells[0].Refs[1];
                this.InitState = new InitialAccountState()
                ***REMOVED***
                    Code = boc.RootCells[0].Refs[0].ToBoc().SerializeToBase64(),
                    Data = boc.RootCells[0].Refs[1].ToBoc().SerializeToBase64()
            ***REMOVED***;
        ***REMOVED***
    ***REMOVED***

        this.PublicKeyBytes = Convert.FromHexString(this.PublicKey);

        var addressSpan = this.Address.AsSpan();
        this.AddressBytes = Convert.FromHexString(addressSpan[2..]);

        if (!uint.TryParse(addressSpan[..1], out var wc))
        ***REMOVED***
            throw new InvalidDataException("Invalid address format: The contract's address does not contain a workchain ID");
    ***REMOVED***

        this.Workchain = wc;
***REMOVED***

    #endregion

    #region Public Fields

    public byte[] AddressBytes ***REMOVED*** get; private set; ***REMOVED***

    public uint Workchain ***REMOVED*** get; private set; ***REMOVED***

    public byte[] PublicKeyBytes ***REMOVED*** get; private set; ***REMOVED***

    public InitialAccountState InitState ***REMOVED*** get; private set; ***REMOVED***

    public Cell Data ***REMOVED*** get; private set; ***REMOVED***

    #endregion
***REMOVED***