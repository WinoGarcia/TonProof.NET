using TonLibDotNet;
using TonLibDotNet.Types;
using TonLibDotNet.Types.Smc;
using TonLibDotNet.Utils;
using TonProof.Extensions;

// ReSharper disable once CheckNamespace
namespace TonProof;

/// <summary>
/// A provider for retrieving public keys from the TON blockchain using the TON client library.
/// </summary>
/// <remarks>
/// The TonClient uses a direct connection to the LiteServer to retrieve the public key.
/// </remarks>
public class TonLibPublicKeyProvider : IPublicKeyProvider, IDisposable
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
    /// <returns></returns>
    public async Task<string> GetPublicKeyAsync(string address, CancellationToken cancellationToken = default)
    {
        await this.tonClient.InitIfNeeded().ConfigureAwait(false);
        await this.tonClient.Sync().ConfigureAwait(false);
        var smc = await this.tonClient.SmcLoadAsync(new AccountAddress(address), cancellationToken).ConfigureAwait(false);

        var smcPublicKey = await this.tonClient
            .SmcRunGetMethodAsync(smc.Id, new MethodIdName("get_public_key"), cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        await this.tonClient.SmcForgetAsync(smc.Id, cancellationToken).ConfigureAwait(false);

        TonLibNonZeroExitCodeException.ThrowIfNonZero(smcPublicKey.ExitCode);

        return Convert.ToHexString(smcPublicKey.Stack[0].ToBigIntegerBytes());
    }

    #endregion

    public void Dispose()
    {
        // TODO release managed resources here
    }
}