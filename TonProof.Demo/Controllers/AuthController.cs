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
{
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
    {
        this.logger = logger;
        this.tonProofService = tonProofService;

        this.jwtSettings = configuration.Value.Jwt;
        this.payloadSetting = configuration.Value.Payload;
    }

    #endregion

    #region Endpoints

    [HttpPost("[action]", Name = "GeneratePayload")]
    [ProducesResponseType(typeof(GeneratePayloadResponse), StatusCodes.Status200OK)]
    public ActionResult<GeneratePayloadResponse> GeneratePayload()
    {
        var payload = this.CreatePayloadToken();

        var response = new GeneratePayloadResponse()
        {
            Payload = payload
        };
        return this.Ok(response);
    }
    
    [HttpPost("[action]", Name = "CheckProof")]
    [ProducesResponseType(typeof(CheckProofResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CheckProofResponse>> CheckProof(CheckProofRequest request, CancellationToken cancellationToken)
    {
        var verifyResult = await this.tonProofService.VerifyAsync(request, cancellationToken);
        if (verifyResult != VerifyResult.Valid)
        {
            return this.BadRequest($"Invalid proof: {Enum.GetName(verifyResult)}");
        }

        var payload = request.Proof.Payload;
        if (!this.ValidateToken(payload))
        {
            return this.BadRequest("Invalid payload");
        }

        var token = this.CreateToken(payload, request.Address);

        return this.Ok(new CheckProofResponse()
        {
            Token = token
        });
    }

    #endregion

    #region Private Methods

    private bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(this.payloadSetting.SecretKey);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = this.payloadSetting.Issuer,
            ValidateAudience = true,
            ValidAudience = this.payloadSetting.Audience,
            ValidateLifetime = true
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal is not null;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Invalid token: {Token}", token);
            return false;
        }
    }

    private string CreatePayloadToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(this.payloadSetting.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddSeconds(this.payloadSetting.Expire),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = this.payloadSetting.Issuer,
            Audience = this.payloadSetting.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

    private string CreateToken(string payload, string address)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(this.jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, payload),
                new(ClaimTypes.Sid, address)
            }),
            Expires = DateTime.UtcNow.AddSeconds(this.jwtSettings.Expire),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = this.jwtSettings.Issuer,
            Audience = this.jwtSettings.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

    #endregion
}