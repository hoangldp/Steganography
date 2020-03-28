using System.Drawing;
using System.Text;

namespace Steganography.Library
{
    public interface ISteganography
    {
        Bitmap Embed(string secret);
        Bitmap Embed(string secret, Encoding encoding);
        string Extract();
        string Extract(Encoding encoding);
    }
}