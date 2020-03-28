using System;
using System.Linq;
using System.Text;
using Steganography.Library.Exception;

namespace Steganography.Library
{
    public static class SecretDataExtension
    {
        public static string GetEmbed(this string secret)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < secret.Length; i++)
            {
                int b = secret[i];
                string data = Convert.ToString(b, 2);
                result.Append(data.PadLeft(8, '0'));
            }

            return result.ToString();
        }

        public static string GetEmbed(this string secret, Encoding encoding)
        {
            if (encoding == null) return GetEmbed(secret);

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < secret.Length; i++)
            {
                string b = secret[i].ToString();
                var bytes = encoding.GetBytes(b);
                string data = string.Join("", bytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
                result.Append(data.PadLeft(32, '0'));
            }

            return result.ToString();
        }

        public static string GetExtract(this string data, Encoding encoding)
        {
            if (encoding == null) return GetExtract(data);

            int length = data.Length;
            if (length % 32 != 0) throw new ExtractException();

            StringBuilder result = new StringBuilder();

            int i = 0;
            while (i * 32 < length)
            {
                string secret = data.Substring(i * 32, 32);
                string[] str = new string[4];
                for (int j = 0; j < 4; j++)
                {
                    str[j] = secret.Substring(j * 8, 8);
                }

                byte[] bytes = str
                    .Select(x => (byte) Convert.ToInt32(x, 2))
                    .Where(x => x > 0)
                    .ToArray();
                result.Append(encoding.GetString(bytes));
                i++;
            }

            return result.ToString();
        }

        public static string GetExtract(this string data)
        {
            int length = data.Length;
            if (length % 8 != 0) throw new ExtractException();

            StringBuilder result = new StringBuilder();

            int i = 0;
            while (i * 8 < length)
            {
                string secret = data.Substring(i * 8, 8);
                int b = Convert.ToInt32(secret, 2);
                result.Append((char)b);
                i++;
            }

            return result.ToString();
        }
    }
}