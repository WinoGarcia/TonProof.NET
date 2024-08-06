using TonLibDotNet;
using TonLibDotNet.Types;
using TonLibDotNet.Types.Smc;
using TonLibDotNet.Utils;

// ReSharper disable once CheckNamespace
namespace TonProof;

/// <summary>
/// A provider for retrieving public keys from the TON blockchain using the TON client library.
/// </summary>
/// <remarks>
/// The TonClient uses a direct connection to the LiteServer to retrieve the public key.
/// </remarks>
public class TonLibPublicKeyProvider : IPublicKeyProvider
{
    #region Private Fields

    private readonly ITonClient tonClient;

    #endregion

    #region Constructors

    public TonLibPublicKeyProvider(ITonClient tonClient)
    {
        this.tonClient = tonClient;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Retrieves the public key for the specified address by invoking the <c>get_public_key</c> method through the lite server.
    /// </summary>
    /// <param name="address">The address of the wallet.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The public key in hex format.</returns>
    /// <exception cref="TonLibNonZeroExitCodeException">Thrown if the request to the lite server was unsuccessful.</exception>
    public async Task<string> GetPublicKeyAsync(string address, CancellationToken cancellationToken = default)
    {
        await this.tonClient.InitIfNeeded(cancellationToken).ConfigureAwait(false);
        await this.tonClient.Sync().ConfigureAwait(false);
        var smc = await this.tonClient.SmcLoad(new AccountAddress(address)).ConfigureAwait(false);

        var smcPublicKey = await this.tonClient.SmcRunGetMethod(smc.Id, new MethodIdName("get_public_key")).ConfigureAwait(false);
        await this.tonClient.SmcForget(smc.Id).ConfigureAwait(false);

        TonLibNonZeroExitCodeException.ThrowIfNonZero(smcPublicKey.ExitCode);

        return Convert.ToHexString(smcPublicKey.Stack[0].ToBigIntegerBytes());
    }

    #endregion
}