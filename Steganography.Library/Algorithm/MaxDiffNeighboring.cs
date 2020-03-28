using System;
using System.Drawing;
using System.Text;
using Steganography.Library.Exception;
using Steganography.Library.Image;

namespace Steganography.Library.Algorithm
{
    public enum ProposedMethod
    {
        FiveNeighborDifferencing,
        SixNeighborDifferencing,
        SevenNeighborDifferencing,
        EightNeighborDifferencing
    }

    public class MaxDiffNeighboring : ISteganography
    {
        private Bitmap _bitmap;
        private ProposedMethod _proposedMethod;

        private string _strStop;

        public MaxDiffNeighboring(Bitmap bitmap, ProposedMethod proposedMethod)
        {
            _bitmap = bitmap;
            _proposedMethod = proposedMethod;
        }

        public Bitmap Embed(string secret)
        {
            return this.Embed(secret, null);
        }

        public Bitmap Embed(string secret, Encoding encoding)
        {
            _strStop = "".PadLeft(encoding == null ? 16 : 64, '0');

            int width = _bitmap.Width;
            int height = _bitmap.Height;
            string data = secret.GetEmbed(encoding) + _strStop;
            int pos = 0;

            bool stop = false;
            for (int i = 1; i < width; i += 2)
            {
                for (int j = 1; j < height; j += 2)
                {
                    Color p = _bitmap.GetPixel(i, j);
                    byte g = p.GetGray();

                    Color pUpper = _bitmap.GetPixel(i, j - 1);
                    Color pLeft = _bitmap.GetPixel(i - 1, j);
                    Color pRight = _bitmap.GetPixel(i + 1, j);
                    Color pBottom = _bitmap.GetPixel(i, j + 1);
                    Color pUpperLeft = _bitmap.GetPixel(i - 1, j - 1);
                    Color pUpperRight = _bitmap.GetPixel(i + 1, j - 1);
                    Color pBottomLeft = _bitmap.GetPixel(i - 1, j + 1);
                    Color pBottomRight = _bitmap.GetPixel(i + 1, j + 1);

                    byte n;
                    switch (_proposedMethod)
                    {
                        case ProposedMethod.FiveNeighborDifferencing:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperRight);
                            break;
                        case ProposedMethod.SixNeighborDifferencing:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperLeft, pUpperRight);
                            break;
                        case ProposedMethod.SevenNeighborDifferencing:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperLeft, pUpperRight, pBottomLeft);
                            break;
                        case ProposedMethod.EightNeighborDifferencing:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperLeft, pUpperRight, pBottomLeft, pBottomRight);
                            break;
                        default:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperLeft, pUpperRight, pBottomLeft, pBottomRight);
                            break;
                    }

                    int adding = data.Length - pos - n;
                    stop = adding < 0;
                    if (stop) adding = -1 * adding;
                    else adding = 0;

                    string embed = data.Substring(pos, n - adding);
                    if (adding > 0) embed = embed + "".PadLeft(adding, '0');

                    int b = Convert.ToInt32(embed, 2);
                    byte gNew = p.CalculateGrayEmbed(n, b);

                    Color pNew = p.ChangeGray(gNew - g);
                    _bitmap.SetPixel(i, j, pNew);

                    pos += n;
                    if (stop) break;

                    stop = pos >= data.Length;
                    if (stop) break;
                }

                if (stop) break;
            }

            if (pos < data.Length) throw new SteganographyException(SteganographyError.NotEnoughToEmbed);

            return _bitmap;
        }

        public string Extract()
        {
            return this.Extract(null);
        }

        public string Extract(Encoding encoding)
        {
            _strStop = "".PadLeft(encoding == null ? 16 : 64, '0');
            int lengthStop = _strStop.Length;

            StringBuilder result = new StringBuilder();
            int width = _bitmap.Width;
            int height = _bitmap.Height;
            int pos = 0;

            bool stop = false;
            for (int i = 1; i + 1 < width; i += 2)
            {
                for (int j = 1; j + 1 < height; j += 2)
                {
                    Color p = _bitmap.GetPixel(i, j);
                    byte g = p.GetGray();

                    Color pUpper = _bitmap.GetPixel(i, j - 1);
                    Color pLeft = _bitmap.GetPixel(i - 1, j);
                    Color pRight = _bitmap.GetPixel(i + 1, j);
                    Color pBottom = _bitmap.GetPixel(i, j + 1);
                    Color pUpperLeft = _bitmap.GetPixel(i - 1, j - 1);
                    Color pUpperRight = _bitmap.GetPixel(i + 1, j - 1);
                    Color pBottomLeft = _bitmap.GetPixel(i - 1, j + 1);
                    Color pBottomRight = _bitmap.GetPixel(i + 1, j + 1);

                    byte n;
                    switch (_proposedMethod)
                    {
                        case ProposedMethod.FiveNeighborDifferencing:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperRight);
                            break;
                        case ProposedMethod.SixNeighborDifferencing:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperLeft, pUpperRight);
                            break;
                        case ProposedMethod.SevenNeighborDifferencing:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperLeft, pUpperRight, pBottomLeft);
                            break;
                        case ProposedMethod.EightNeighborDifferencing:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperLeft, pUpperRight, pBottomLeft, pBottomRight);
                            break;
                        default:
                            n = p.GetCapacityEmbed(pUpper, pLeft, pRight, pBottom, pUpperLeft, pUpperRight, pBottomLeft, pBottomRight);
                            break;
                    }
                    int b = g.GetDataExtract(n);

                    string secret = Convert.ToString(b, 2);
                    result.Append(secret.PadLeft(n, '0'));

                    int length = result.Length;
                    stop = length >= lengthStop
                           && result.ToString().Substring(length - lengthStop, lengthStop).Equals(_strStop);

                    if (stop) break;
                }

                if (stop) break;
            }

            if (!stop) throw new SteganographyException(SteganographyError.NotExtract);

            string extract = result.ToString();
            int subtracting = extract.Length % (lengthStop / 2);
            if (subtracting > 0) extract = extract.Substring(0, extract.Length - subtracting - _strStop.Length);
            return extract.GetExtract(encoding);
        }
    }
}