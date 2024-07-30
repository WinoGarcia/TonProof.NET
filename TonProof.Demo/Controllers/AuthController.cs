using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TonProof.Demo.Types;
using TonProof.Types;

namespace TonProof.Demo.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ApiExplorerSettings(GroupName = "auth")]
public sealed class AuthController : ControllerBase
***REMOVED***
    #region Private Fields

    private readonly ILogger<AuthController> logger;
    private readonly ITonProofService tonProofService;

    private readonly TokenSettings jwtSettings;
    private readonly TokenSettings payloadSetting;

    #endregion

    #region Constructors

    public AuthController(
        ILogger<AuthController> logger,
        IOptions<SettingOptions> configuration,
        ITonProofService tonProofService)
    ***REMOVED***
        this.logger = logger;
        this.tonProofService = tonProofService;

        this.jwtSettings = configuration.Value.Jwt;
        this.payloadSetting = configuration.Value.Payload;
***REMOVED***

    #endregion

    #region Endpoints

    [HttpPost("[action]", Name = "GeneratePayload")]
    [ProducesResponseType(typeof(GeneratePayloadResponse), StatusCodes.Status200OK)]
    public ActionResult<GeneratePayloadResponse> GeneratePayload()
    ***REMOVED***
        var payload = this.CreatePayloadToken();

        var response = new GeneratePayloadResponse()
        ***REMOVED***
            Payload = payload
    ***REMOVED***;
        return this.Ok(response);
***REMOVED***
    
    [HttpPost("[action]", Name = "CheckProof")]
    [ProducesResponseType(typeof(CheckProofResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CheckProofResponse>> CheckProof(CheckProofRequest request, CancellationToken cancellationToken)
    ***REMOVED***
        var verifyResult = await this.tonProofService.VerifyAsync(request, cancellationToken);
        if (verifyResult != VerifyResult.Valid)
        ***REMOVED***
            return this.BadRequest($"Invalid proof: ***REMOVED***Enum.GetName(verifyResult)***REMOVED***");
    ***REMOVED***

        var payload = request.Proof.Payload;
        if (!this.ValidateToken(payload))
        ***REMOVED***
            return this.BadRequest("Invalid payload");
    ***REMOVED***

        var token = this.CreateToken(payload, request.Address);

        return this.Ok(new CheckProofResponse()
        ***REMOVED***
            Token = token
    ***REMOVED***);
***REMOVED***

    #endregion

    #region Private Methods

    private bool ValidateToken(string token)
    ***REMOVED***
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(this.payloadSetting.SecretKey);
        var validationParameters = new TokenValidationParameters
        ***REMOVED***
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = this.payloadSetting.Issuer,
            ValidateAudience = true,
            ValidAudience = this.payloadSetting.Audience,
            ValidateLifetime = true
    ***REMOVED***;

        try
        ***REMOVED***
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal is not null;
    ***REMOVED***
        catch (Exception ex)
        ***REMOVED***
            this.logger.LogError(ex, "Invalid token: ***REMOVED***Token***REMOVED***", token);
            return false;
    ***REMOVED***
***REMOVED***

    private string CreatePayloadToken()
    ***REMOVED***
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(this.payloadSetting.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        ***REMOVED***
            Subject = new ClaimsIdentity(new Claim[]
            ***REMOVED***
                new(ClaimTypes.Name, Guid.NewGuid().ToString())
        ***REMOVED***),
            Expires = DateTime.UtcNow.AddSeconds(this.payloadSetting.Expire),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = this.payloadSetting.Issuer,
            Audience = this.payloadSetting.Audience
    ***REMOVED***;
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
***REMOVED***

    private string CreateToken(string payload, string address)
    ***REMOVED***
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(this.jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        ***REMOVED***
            Subject = new ClaimsIdentity(new Claim[]
            ***REMOVED***
                new(ClaimTypes.Name, payload),
                new(ClaimTypes.Sid, address)
        ***REMOVED***),
            Expires = DateTime.UtcNow.AddSeconds(this.jwtSettings.Expire),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = this.jwtSettings.Issuer,
            Audience = this.jwtSettings.Audience
    ***REMOVED***;
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
***REMOVED***

    #endregion
***REMOVED***