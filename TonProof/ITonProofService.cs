using TonProof.Types;

namespace TonProof;

/// <summary>
/// Provides functionality to verify proofs in Ton Connect.
/// </summary>
public interface ITonProofService
{
    /// <summary>
    /// Verifies the proof based on the given request.
    /// </summary>
    /// <param name="request">The request containing proof data to be verified.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <see href="https://docs.ton.org/develop/dapps/ton-connect/sign"/>
    /// <seealso href="https://github.com/ton-connect/demo-dapp-backend"/>
    /// <returns>
    /// <see cref="VerifyResult"/> indicating the outcome of the verification.
    /// </returns>
    Task<VerifyResult> VerifyAsync(CheckProofRequest request, CancellationToken cancellationToken = default);
}