using System.Text.Json.Serialization;

namespace TonProof.Types;

/// <summary>
/// Represents an object for proof verification.
/// <see href="https://docs.ton.org/develop/dapps/ton-connect/sign#structure-of-ton_proof"/>
/// <seealso href="https://github.com/ton-connect/demo-dapp-backend?tab=readme-ov-file"/>
/// </summary>
public class CheckProofRequest
***REMOVED***
    [JsonPropertyName("address")]
    public string Address ***REMOVED*** get; set; ***REMOVED***
    
    [JsonPropertyName("network")]
    public string Network ***REMOVED*** get; set; ***REMOVED***
    
    [JsonPropertyName("public_key")]
    public string PublicKey ***REMOVED*** get; set; ***REMOVED***
    
    [JsonPropertyName("proof")]
    public Proof Proof ***REMOVED*** get; set; ***REMOVED***
***REMOVED***

public record Proof
***REMOVED***
    [JsonPropertyName("timestamp")]
    public long Timestamp ***REMOVED*** get; set; ***REMOVED***
    
    [JsonPropertyName("domain")]
    public Domain Domain ***REMOVED*** get; set; ***REMOVED***
    
    [JsonPropertyName("signature")]
    public string Signature ***REMOVED*** get; set; ***REMOVED***
    
    [JsonPropertyName("payload")]
    public string Payload ***REMOVED*** get; set; ***REMOVED***
    
    [JsonPropertyName("state_init")]
    public string StateInit ***REMOVED*** get; set; ***REMOVED***
***REMOVED***

public record Domain
***REMOVED***
    [JsonPropertyName("LengthBytes")]
    public uint LengthBytes ***REMOVED*** get; set; ***REMOVED***

    [JsonPropertyName("value")]
    public string Value ***REMOVED*** get; set; ***REMOVED***
***REMOVED***