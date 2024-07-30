namespace TonProof.Demo.Types;

public class SettingOptions
{
    public const string Tokens = "Tokens";
    
    public TokenSettings Payload { get; set; }
    
    public TokenSettings Jwt { get; set; }
}

public class TokenSettings
{
    public string Issuer { get; set; }
    public string SecretKey { get; set; }
    public string Audience { get; set; }
    public int Expire { get; set; }
}