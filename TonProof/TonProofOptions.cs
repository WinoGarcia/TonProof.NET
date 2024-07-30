using TonProof.Types.Wallets;

namespace TonProof;

/// <summary>
/// Provides configuration options for proof verification.
/// </summary>
public class TonProofOptions
***REMOVED***
    /// <summary>
    /// The prefix used in TonConnect for identification.
    /// </summary>
    public string TonConnectPrefix ***REMOVED*** get; set; ***REMOVED*** = "ton-connect";

    /// <summary>
    /// The prefix used for proof items in the system.
    /// </summary>
    public string TonProofPrefix ***REMOVED*** get; set; ***REMOVED*** = "ton-proof-item-v2/";

    /// <summary>
    /// Maximum allowed time (in seconds) for a proof to be considered valid.
    /// Default is 15 minutes (900 seconds).
    /// </summary>
    public long ValidAuthTime ***REMOVED*** get; set; ***REMOVED*** = 15 * 60; // 15 minutes

    /// <summary>
    /// A collection of allowed domains that are considered valid for proof verification.
    /// </summary>
    public IEnumerable<string> AllowedDomains ***REMOVED*** get; set; ***REMOVED*** = Enumerable.Empty<string>();

    /// <summary>
    /// A dictionary mapping known wallet contract codes to their corresponding creation functions.
    /// </summary>
    public Dictionary<string, Func<IWalletContract>> KnownWallets ***REMOVED*** get; set; ***REMOVED*** = new()
    ***REMOVED***
        ***REMOVED*** WalletContractV1R1.CodeBase64, WalletContractV1R1.Create ***REMOVED***,
        ***REMOVED*** WalletContractV1R2.CodeBase64, WalletContractV1R2.Create ***REMOVED***,
        ***REMOVED*** WalletContractV1R3.CodeBase64, WalletContractV1R3.Create ***REMOVED***,

        ***REMOVED*** WalletContractV2R1.CodeBase64, WalletContractV2R1.Create ***REMOVED***,
        ***REMOVED*** WalletContractV2R2.CodeBase64, WalletContractV2R2.Create ***REMOVED***,

        ***REMOVED*** WalletContractV3R1.CodeBase64, WalletContractV3R1.Create ***REMOVED***,
        ***REMOVED*** WalletContractV3R2.CodeBase64, WalletContractV3R2.Create ***REMOVED***,

        ***REMOVED*** WalletContractV4R2.CodeBase64, WalletContractV4R2.Create ***REMOVED***
***REMOVED***;
***REMOVED***