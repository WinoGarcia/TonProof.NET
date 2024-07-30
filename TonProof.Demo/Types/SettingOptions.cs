namespace TonProof.Demo.Types;

public class SettingOptions
***REMOVED***
    public const string Tokens = "Tokens";
    
    public TokenSettings Payload ***REMOVED*** get; set; ***REMOVED***
    
    public TokenSettings Jwt ***REMOVED*** get; set; ***REMOVED***
***REMOVED***

public class TokenSettings
***REMOVED***
    public string Issuer ***REMOVED*** get; set; ***REMOVED***
    public string SecretKey ***REMOVED*** get; set; ***REMOVED***
    public string Audience ***REMOVED*** get; set; ***REMOVED***
    public int Expire ***REMOVED*** get; set; ***REMOVED***
***REMOVED***