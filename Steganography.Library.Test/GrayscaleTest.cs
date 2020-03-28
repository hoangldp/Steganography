using System.Drawing;
using NUnit.Framework;
using Steganography.Library.Image;

namespace Steganography.Library.Test
{
    public class GrayscaleTest
    {
        private static readonly object[] _dataCalculateGray =
        {
            new object[] { (byte) 12, (byte) 15, (byte) 18, (byte) 14 }, // 14.445
            new object[] { (byte) 12, (byte) 16, (byte) 18, (byte) 15 }, // 15.032
            new object[] { (byte) 12, (byte) 17, (byte) 18, (byte) 16 } // 15.619
        };

        private static readonly object[] _dataCalculateColor =
        {
            new object[] { Color.FromArgb(107, 50, 203), 15, Color.FromArgb(126, 58, 239) },
            new object[] { Color.FromArgb(107, 51, 203), 15, Color.FromArgb(126, 60, 239) },
            new object[] { Color.FromArgb(107, 54, 203), 15, Color.FromArgb(125, 64, 238) },

            new object[] { Color.FromArgb(107, 55, 203), -15, Color.FromArgb(89, 45, 168) },
            new object[] { Color.FromArgb(107, 56, 203), -15, Color.FromArgb(89, 46, 168) },
            new object[] { Color.FromArgb(107, 57, 203), -15, Color.FromArgb(89, 48, 169) }
        };

        [TestCaseSource("_dataCalculateGray")]
        public void CalculateGray(byte r, byte g, byte b, byte gray)
        {
            // Arrange
            Color color = Color.FromArgb(r, g, b);

            // Act
            byte grayCalculate = color.GetGray();

            // Assert
            Assert.AreEqual(grayCalculate, gray);
        }

        [TestCaseSource("_dataCalculateColor")]
        public void CalculateColor(Color originColor, int d, Color color)
        {
            Color newColor = originColor.ChangeGray(d);

            Assert.AreEqual(newColor.R, color.R);
            Assert.AreEqual(newColor.G, color.G);
            Assert.AreEqual(newColor.B, color.B);
        }
    }
}