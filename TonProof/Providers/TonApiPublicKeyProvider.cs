using System.Net.Http.Json;
using TonProof.Types;

// ReSharper disable once CheckNamespace
namespace TonProof;

/// <summary>
/// Provider for retrieving the public key using the Ton Api.
/// </summary>
public class TonApiPublicKeyProvider : IPublicKeyProvider
{
    #region Private Fields

    private readonly HttpClient httpClient;

    #endregion

    #region Constructors

    public TonApiPublicKeyProvider(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Retrieves the public key for the specified address by invoking the <c>get_public_key</c> using the Ton Api.
    /// </summary>
    /// <param name="address">The address of the wallet.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The public key in hex format.</returns>
    /// <exception cref="HttpRequestException">Thrown if the request to the API was unsuccessful.</exception>
    public async Task<string> GetPublicKeyAsync(string address, CancellationToken cancellationToken = default)
    {
        var url = $"{this.httpClient.BaseAddress}/v2/accounts/{Uri.EscapeDataString(address)}/publickey";

        var response = await this.httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        var publicKeyResponse = await response.Content.ReadFromJsonAsync<TonApiPublicKeyResponse>(cancellationToken: cancellationToken);

        return publicKeyResponse.PublicKey;
    }

    #endregion
}