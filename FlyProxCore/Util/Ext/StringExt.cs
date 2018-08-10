using System;
using System.Linq;

namespace FlyProxCore.Util.Ext
{
    public static class StringExt
    {
        public static byte[] ToByteArrayFromHexString(this string hex)
        {
            hex = hex.Replace(" ", "").Replace("\r\n", "");
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}