// ReSharper disable once CheckNamespace
namespace TonProof;

/// <summary>
/// Provides functionality to retrieve public keys associated with addresses.
/// </summary>
public interface IPublicKeyProvider
{
    /// <summary>
    /// Retrieves the public key associated with the specified address.
    /// </summary>
    /// <param name="address">The address of the wallet.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The public key in hex format.</returns>
    Task<string> GetPublicKeyAsync(string address, CancellationToken cancellationToken = default);
}