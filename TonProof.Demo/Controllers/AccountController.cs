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
***REMOVED***
    #region Private Fields

    private readonly ITonClient tonClient;

    #endregion

    #region Constructors

    public AccountController(ITonClient tonClient)
    ***REMOVED***
        this.tonClient = tonClient;
***REMOVED***

    #endregion

    #region Endpoints

    [HttpPost("[action]", Name = "Balance")]
    [ProducesResponseType(typeof(BalanceResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<BalanceResponse>> GetBalance(
        CancellationToken cancellationToken)
    ***REMOVED***
        await this.tonClient.InitIfNeededAsync(cancellationToken);

        var address = this.GetUserAddress();
        var ast = await this.tonClient.GetAccountState(address);

        var response = new BalanceResponse
        ***REMOVED***
            Amount = ast.Balance.ToString()
    ***REMOVED***;
        return this.Ok(response);
***REMOVED***

    #endregion

    #region Private Methods

    private string GetUserAddress()
    ***REMOVED***
        if (this.HttpContext.User.Claims is ***REMOVED******REMOVED*** claims)
        ***REMOVED***
            var sid = claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid);
            if (sid is not null)
            ***REMOVED***
                return sid.Value;
        ***REMOVED***
    ***REMOVED***

        return string.Empty;
***REMOVED***

    #endregion
***REMOVED***