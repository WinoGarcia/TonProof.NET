using TonLibDotNet.Utils;

namespace TonProof.Extensions;

public static class AddressUtilsExtension
{
    public static bool AddressEquals(this AddressUtils self, string address, ReadOnlySpan<byte> accountId)
    {
        self.IsValid(address, out var wc, out var b, out var t);
        var madeAddress = AddressValidator.MakeAddress(wc, accountId, b, t);

        return madeAddress.Equals(address);
    }
}