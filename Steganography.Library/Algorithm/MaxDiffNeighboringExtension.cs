using System;
using System.Drawing;
using Steganography.Library.Image;

namespace Steganography.Library.Algorithm
{
    public static class MaxDiffNeighboringExtension
    {
        public static byte GetCapacityEmbed(this Color color, Color upper, Color left, Color right, Color bottom, Color upperRight)
        {
            byte gUpper = upper.GetGray();
            byte gLeft = left.GetGray();
            byte gRight = right.GetGray();
            byte gBottom = bottom.GetGray();
            byte gUpperRight = upperRight.GetGray();

            byte gMax = Math.Max(gUpper, Math.Max(gLeft, Math.Max(gRight, Math.Max(gBottom, gUpperRight))));
            byte gMin = Math.Min(gUpper, Math.Min(gLeft, Math.Min(gRight, Math.Min(gBottom, gUpperRight))));

            int d = gMax - gMin;
            if (d <= 1) return 1;

            double n = Math.Floor(Math.Log(d, 2));
            if (n > 4) return 4;

            return (byte) n;
        }

        public static byte GetCapacityEmbed(this Color color, Color upper, Color left, Color right, Color bottom, Color upperLeft, Color upperRight)
        {
            byte gUpper = upper.GetGray();
            byte gLeft = left.GetGray();
            byte gRight = right.GetGray();
            byte gBottom = bottom.GetGray();
            byte gUpperLeft = upperLeft.GetGray();
            byte gUpperRight = upperRight.GetGray();

            byte gMax = Math.Max(gUpper, Math.Max(gLeft, Math.Max(gRight, Math.Max(gBottom, Math.Max(gUpperLeft, gUpperRight)))));
            byte gMin = Math.Min(gUpper, Math.Min(gLeft, Math.Min(gRight, Math.Min(gBottom, Math.Min(gUpperLeft, gUpperRight)))));

            int d = gMax - gMin;
            if (d <= 1) return 1;

            double n = Math.Floor(Math.Log(d, 2));
            if (n > 4) return 4;

            return (byte) n;
        }

        public static byte GetCapacityEmbed(this Color color, Color upper, Color left, Color right, Color bottom, Color upperLeft, Color upperRight, Color bottomLeft)
        {
            byte gUpper = upper.GetGray();
            byte gLeft = left.GetGray();
            byte gRight = right.GetGray();
            byte gBottom = bottom.GetGray();
            byte gUpperLeft = upperLeft.GetGray();
            byte gUpperRight = upperRight.GetGray();
            byte gBottomLeft = bottomLeft.GetGray();

            byte gMax = Math.Max(gUpper, Math.Max(gLeft, Math.Max(gRight, Math.Max(gBottom, Math.Max(gUpperLeft, Math.Max(gUpperRight, gBottomLeft))))));
            byte gMin = Math.Min(gUpper, Math.Min(gLeft, Math.Min(gRight, Math.Min(gBottom, Math.Min(gUpperLeft, Math.Min(gUpperRight, gBottomLeft))))));

            int d = gMax - gMin;
            if (d <= 1) return 1;

            double n = Math.Floor(Math.Log(d, 2));
            if (n > 4) return 4;

            return (byte) n;
        }

        public static byte GetCapacityEmbed(this Color color, Color upper, Color left, Color right, Color bottom, Color upperLeft, Color upperRight, Color bottomLeft, Color bottomRight)
        {
            byte gUpper = upper.GetGray();
            byte gLeft = left.GetGray();
            byte gRight = right.GetGray();
            byte gBottom = bottom.GetGray();
            byte gUpperLeft = upperLeft.GetGray();
            byte gUpperRight = upperRight.GetGray();
            byte gBottomLeft = bottomLeft.GetGray();
            byte gBottomRight = bottomRight.GetGray();

            byte gMax = Math.Max(gUpper, Math.Max(gLeft, Math.Max(gRight, Math.Max(gBottom, Math.Max(gUpperLeft, Math.Max(gUpperRight, Math.Max(gBottomLeft, gBottomRight)))))));
            byte gMin = Math.Min(gUpper, Math.Min(gLeft, Math.Min(gRight, Math.Min(gBottom, Math.Min(gUpperLeft, Math.Min(gUpperRight, Math.Min(gBottomLeft, gBottomRight)))))));

            int d = gMax - gMin;
            if (d <= 1) return 1;

            double n = Math.Floor(Math.Log(d, 2));
            if (n > 4) return 4;

            return (byte) n;
        }

        public static byte CalculateGrayEmbed(this Color color, byte n, int b)
        {
            byte gray = color.GetGray();
            byte d = (byte) Math.Pow(2, n);
            int newGray = gray - (gray % d) + b;
            int delta = newGray - gray;

            int bottom = (int) Math.Pow(2, n - 1);
            int upper = (int) Math.Pow(2, n);

            if (newGray >= upper && delta > bottom && delta < upper) newGray = newGray - upper;
            if (newGray < 256 - upper && delta > -1 * upper && delta < -1 * bottom) newGray = newGray + upper;

            return (byte) newGray;
        }

        public static int GetDataExtract(this byte gray, byte n)
        {
            double d = Math.Pow(2, n);
            double b = gray % d;
            return (int) b;
        }
    }
}