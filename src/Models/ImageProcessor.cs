using System.Drawing;
using System.IO;
using System.Text;
using System;
namespace Tubes3.Models
{
    public static class ImageProcessor
    {
        /// <summary>
        /// Mengubah gambar menjadi binary
        /// </summary>
        /// <param name="bitmap"> Bitmap yang ingin diubah menjadi binary </param>
        /// <returns> Binary dari gambar dalam string </returns>
        public static string ConvertBitmapToAscii(FileStream stream)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            var bitmap = new Bitmap(stream);
#pragma warning restore CA1416 // Validate platform compatibility
            StringBuilder asciiArt = new StringBuilder();

#pragma warning disable CA1416 // Validate platform compatibility
            for (int y = 0; y < bitmap.Height; y++)
            {
#pragma warning disable CA1416 // Validate platform compatibility
                for (int x = 0; x < bitmap.Width; x++)
                {
#pragma warning disable CA1416 // Validate platform compatibility
                    var pixelColor = bitmap.GetPixel(x, y);
#pragma warning restore CA1416 // Validate platform compatibility
                    int grayValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    var binaryString = Convert.ToString(grayValue, 2).PadLeft(8, '0');
                    var asciiChar = BinaryToAscii(binaryString);
                    asciiArt.Append(asciiChar);
                }
#pragma warning restore CA1416 // Validate platform compatibility
                asciiArt.Append(Environment.NewLine);
            }
#pragma warning restore CA1416 // Validate platform compatibility

            return asciiArt.ToString();
        }

        private static char BinaryToAscii(string binary)
        {
            int decimalValue = Convert.ToInt32(binary, 2);
            return (char)decimalValue;
        }

        /// <summary>
        /// Mengubah bitmap menjadi grayscale dan mengembalikan nilai intensitas
        /// </summary>
        /// <param name="color">Warna piksel</param>
        /// <returns>Nilai intensitas grayscale</returns>
        private static int ToGrayscale(Color color)
        {
            return (color.R + color.G + color.B) / 3;
        }

    }
}
