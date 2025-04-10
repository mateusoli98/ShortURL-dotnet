using System.Numerics;
using System.Text;

namespace ShortURL_API.Extensions;

public static class GuidConvertionExtension
{
    private const string base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const int Base62Length = 62;

    /// <summary>
    /// Converts a Guid to a Base62 string representation.
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string EncodeGuidToBase62(this Guid guid, int maxLength = 10)
    {
        var bytes = guid.ToByteArray();

        // A zero byte is concatenated to ensure the byte array is treated as an unsigned integer
        // when converted to a BigInteger, avoiding negative values.
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
