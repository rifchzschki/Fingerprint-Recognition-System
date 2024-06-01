using System.Drawing;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

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
                    int grayValue = ToGrayscale(pixelColor);
                    var binaryString = Convert.ToString(grayValue, 2).PadLeft(8, '0');
                    var asciiChar = BinaryToAscii(binaryString);
                    asciiArt.Append(asciiChar);
                }
                asciiArt.Append(Environment.NewLine);
            }

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

        /// <summary>
        /// Mengambil pattern ASCII di baris tengah
        /// </summary>
        /// <param name="ascii">ASCII yang ingin diambil patternnya</param>
        /// <returns>Pattern dari ASCII</returns>
        public static string GetPatternFromAscii(string ascii)
        {
            var lines = ascii.Split(Environment.NewLine);
            var middleLine = lines[lines.Length / 2];
            var middleLineLength = middleLine.Length;
            var pattern = middleLine.Substring(middleLineLength / 2 - 15, 30);
            return pattern;
        }

        // Process all images in the database to ASCII
        public static Dictionary<string, (string, string?)> ProcessImagesToAscii()
        {
            var dbHelper = new DatabaseHelper();
            List<SidikJari> sidikJaris = dbHelper.GetSidikJaris();
            // Test.TestHere(sidikJaris);

            var imageAsciiMap = new Dictionary<string?, (string, string?)>();

            foreach (SidikJari sidik in sidikJaris)
            {
                // Check if sidik.Berkas_citra not null and the file exists
                if (!File.Exists("../" + sidik.Berkas_citra) )
                {
                    // Print error (tapi malah gbs ke print anjir)
                    Console.WriteLine($"Path img null or file doesn't exist: {sidik.Berkas_citra}");
                    continue;
                }
                // Test.TestHere("../"+sidik.Berkas_citra.ToString());
                using (var stream = File.OpenRead("../" + sidik.Berkas_citra))
                {
                    // Test.TestHere("../"+sidik.Berkas_citra);
                    var result = ConvertBitmapToAscii(stream);
                    imageAsciiMap[result] = (sidik.Berkas_citra, sidik.Nama);
                }
            }

            return imageAsciiMap;
        }

    }
}
