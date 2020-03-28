using NUnit.Framework;

namespace Steganography.Library.Test
{
    public class SecretTest
    {
        private static readonly object[] _dataSecret =
        {
            new object[] { "z", "01111010" },
            new object[] { "A", "01000001" },
            new object[] { "%", "00100101" },

            new object[] { "A%", "0100000100100101" },
            new object[] { "A%z", "010000010010010101111010" }
        };

        [TestCaseSource("_dataSecret")]
        public void Embed(string secret, string data)
        {
            Assert.AreEqual(secret.GetEmbed(), data);
        }

        [TestCaseSource("_dataSecret")]
        public void Extract(string secret, string data)
        {
            Assert.AreEqual(data.GetExtract(), secret);
        }
    }
}