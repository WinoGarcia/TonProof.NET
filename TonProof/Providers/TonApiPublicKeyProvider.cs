using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using TonProof.Types;

namespace TonProof;

public class TonApiPublicKeyProvider : IPublicKeyProvider
{
    #region Private Fields

    private readonly ILogger<TonApiPublicKeyProvider> logger;
    private readonly HttpClient httpClient;

    #endregion

    #region Constructors

    public TonApiPublicKeyProvider(
        ILogger<TonApiPublicKeyProvider> logger,
        HttpClient httpClient)
    {
        this.logger = logger;
        this.httpClient = httpClient;
    }

    #endregion

    #region Public Methods

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