using Avalonia.Media.Imaging;
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
        public static string ConvertImageToBinary(Bitmap bitmap)
        {
            using var stream = new MemoryStream();
            bitmap.Save(stream);
            var bytes = stream.ToArray();
            var binary = new StringBuilder();
            foreach (var b in bytes)
            {
                binary.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return binary.ToString();
        }

        /// <summary>
        /// Mengubah binary menjadi decimal
        /// </summary>
        /// <param name="n"> Binary yang ingin diubah menjadi decimal </param>
        /// <returns> Decimal dari binary dalam string </returns>
        private static int BinaryToDecimal(string n)
        {
            string num = n;
            int dec_value = 0;
            int base1 = 1;

            int len = num.Length;
            for (int i = len - 1; i >= 0; i--)
            {

                if (num[i] == '1')
                    dec_value += base1;
                base1 = base1 * 2;
            }

            return dec_value;
        }

        /// <summary>
        /// Mengubah string menjadi ASCII
        /// </summary>
        /// <param name="str"> String Binary yang ingin diubah menjadi ASCII 8 - bit </param>
        /// <returns> ASCII - 8 bit dari string </returns>
        public static string SetStringtoASCII(string str)
        {
            int N = str.Length;

            // if (N % 8 != 0)
            // {
            //     return "Not Possible!";
            // }

            string res = "";

            for (int i = 0; i < N; i += 8)
            {
                int decimal_value
                    = BinaryToDecimal(str.Substring(i, 8));

                res += (char)decimal_value;
            }

            return res;
        }

    }
}
