using System.Drawing;

namespace Steganography.Library
{
    public static class SteganographyExtension
    {
        public static ISteganography MaxDiffNeighboring(this Bitmap image, ProposedMethod proposedMethod)
        {
            return new MaxDiffNeighboring(image, proposedMethod);
        }
    }
}