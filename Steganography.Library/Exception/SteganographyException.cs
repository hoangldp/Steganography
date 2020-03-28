namespace Steganography.Library.Exception
{
    public enum SteganographyError
    {
        NotEnoughToEmbed,
        NotExtract
    }

    public class SteganographyException : System.Exception
    {
        private SteganographyError _error;

        public SteganographyException(SteganographyError error)
        {
            _error = error;
        }

        public override string Message
        {
            get
            {
                if (_error == SteganographyError.NotEnoughToEmbed) return "Not enough pixel to embed secret.";
                else if (_error == SteganographyError.NotExtract) return "Can't extract secret.";
                else return string.Empty;
            }
        }
    }
}