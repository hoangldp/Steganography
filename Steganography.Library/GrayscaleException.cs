using System;
using System.Drawing;

namespace Steganography.Library
{
    public enum GrayScaleError
    {
        Over,
        NotFound
    }

    public class GrayScaleException : Exception
    {
        private Color _color;
        private int _different;
        private GrayScaleError _error;

        public GrayScaleException(Color color, int different, GrayScaleError error)
        {
            _color = color;
            _different = different;
            _error = error;
        }

        public override string Message
        {
            get
            {
                if (_error == GrayScaleError.Over)
                {
                    return "Different over.";
                }
                else if (_error == GrayScaleError.NotFound)
                {
                    return "Not found color.";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}