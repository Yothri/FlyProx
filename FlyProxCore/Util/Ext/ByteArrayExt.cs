using System.Text;

namespace FlyProxCore.Util.Ext
{
    public static class ByteArrayExt
    {
        public static string ToHexString(this byte[] data)
        {
            StringBuilder hex = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}