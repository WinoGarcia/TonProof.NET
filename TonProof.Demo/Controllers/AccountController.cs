using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TonLibDotNet;
using TonProof.Demo.Types;
using TonProof.Extensions;

namespace TonProof.Demo.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ApiExplorerSettings(GroupName = "account")]
public sealed class AccountController : ControllerBase
{
    #region Private Fields

    private readonly ITonClient tonClient;

    #endregion

    #region Constructors

    public AccountController(ITonClient tonClient)
    {
        this.tonClient = tonClient;
    }

    #endregion

    #region Endpoints

    [HttpPost("[action]", Name = "Balance")]
    [ProducesResponseType(typeof(BalanceResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<BalanceResponse>> GetBalance(
        CancellationToken cancellationToken)
    {
        await this.tonClient.InitIfNeededAsync(cancellationToken);

        var address = this.GetUserAddress();
        var ast = await this.tonClient.GetAccountState(address);

        var response = new BalanceResponse
        {
            Amount = ast.Balance.ToString()
        };
        return this.Ok(response);
    }

    #endregion

    #region Private Methods

    private string GetUserAddress()
    {
        if (this.HttpContext.User.Claims is {} claims)
        {
            var sid = claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid);
            if (sid is not null)
            {
                return sid.Value;
            }
        }

        return string.Empty;
    }

    #endregion
}