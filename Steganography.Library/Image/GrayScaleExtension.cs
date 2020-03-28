using System;
using System.Drawing;
using Steganography.Library.Exception;

namespace Steganography.Library.Image
{
    public static class GrayScaleExtension
    {
        public static byte GetGray(this Color color)
        {
            float grayValue = color.R * 0.299f + color.G * 0.587f + color.B * 0.114f;
            return (byte) Math.Round(grayValue, MidpointRounding.AwayFromZero);
        }

        public static Color ChangeGray(this Color color, int different)
        {
            byte gray = color.GetGray();

            int grayAfter = gray + different;
            if (grayAfter < 0 || grayAfter > 255) throw new GrayScaleException(color, different, GrayScaleError.Over);

            float rate = gray / (float) grayAfter;

            byte r = (byte) Math.Round(color.R / rate, MidpointRounding.AwayFromZero);
            byte g = (byte) Math.Round(color.G / rate, MidpointRounding.AwayFromZero);
            byte b = (byte) Math.Round(color.B / rate, MidpointRounding.AwayFromZero);

            Color newColor = Color.FromArgb(r, g, b);
            byte newGray = newColor.GetGray();

            if (newGray == grayAfter) return newColor;

            int diff = grayAfter - newGray;

            int value = g + diff;
            if (value >= 0 && value <= 255) return Color.FromArgb(r, value, b);

            value = r + diff * 2;
            if (value >= 0 && value <= 255) return Color.FromArgb(value, g, b);

            value = b + diff * 5;
            if (value >= 0 && value <= 255) return Color.FromArgb(r, g, value);

            throw new GrayScaleException(color, different, GrayScaleError.NotFound);
        }
    }
}