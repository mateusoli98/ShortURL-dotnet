using System.Numerics;
using System.Text;

namespace ShortURL_API.Extensions;

public static class GuidConvertionExtension
{
    private const string base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const int Base62Length = 62;   

    public static string EncodeGuidToBase62(this Guid guid, int maxLength = 10)
    {
        var bytes = guid.ToByteArray();
        var number = new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray());

        var sb = new StringBuilder();

        while (number > 0)
        {
            number = BigInteger.DivRem(number, Base62Length, out var remainder);
            sb.Insert(0, base62Chars[(int)remainder]);
        }

        return sb.ToString().Substring(0, Math.Min(maxLength, sb.Length));
    }
}
