using TonLibDotNet;
using TonLibDotNet.Types;
using TonLibDotNet.Types.Smc;
using TonLibDotNet.Types.Ton;
using TonLibDotNet.Types.Tvm;
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

namespace TonProof.Extensions;

public static class TonClientExtension
***REMOVED***
    /// <inheritdoc cref="TonClientExtensions.GetAccountAddress"/>
    public static Task<AccountAddress> GetAccountAddressAsync(
        this ITonClient client,
        InitialAccountState initialAccountState,
        int revision = 0,
        int workchainId = 0,
        CancellationToken cancellationToken = default) =>
        Task.Run(() => client.GetAccountAddress(
                initialAccountState,
                revision,
                workchainId),
            cancellationToken);

    /// <inheritdoc cref="ITonClient.InitIfNeeded"/>
    public static Task<OptionsInfo?> InitIfNeededAsync(this ITonClient client, CancellationToken cancellationToken = default) =>
        Task.Run(client.InitIfNeeded,
            cancellationToken);

    /// <inheritdoc cref="TonClientExtensions.Sync"/>
    public static Task<BlockIdEx> SyncAsync(this ITonClient client, CancellationToken cancellationToken = default) =>
        Task.Run(client.Sync,
            cancellationToken);

    /// <inheritdoc cref="TonClientSmcExtensions.SmcLoad(ITonClient, AccountAddress)"/>
    public static Task<Info> SmcLoadAsync(
        this ITonClient client,
        AccountAddress accountAddress,
        CancellationToken cancellationToken = default) =>
        Task.Run(() => client.SmcLoad(accountAddress),
            cancellationToken);

    /// <inheritdoc cref="TonClientSmcExtensions.SmcRunGetMethod"/>
    public static Task<RunResult> SmcRunGetMethodAsync(
        this ITonClient client,
        long id,
        MethodId methodId,
        List<StackEntry>? stack = null,
        CancellationToken cancellationToken = default) =>
        Task.Run(() => client.SmcRunGetMethod(
                id,
                methodId,
                stack),
            cancellationToken);

    /// <inheritdoc cref="TonClientSmcExtensions.SmcForget"/>
    public static Task<Ok> SmcForgetAsync(this ITonClient client, long id, CancellationToken cancellationToken = default) =>
        Task.Run(() => client.SmcForget(id),
            cancellationToken);
***REMOVED***