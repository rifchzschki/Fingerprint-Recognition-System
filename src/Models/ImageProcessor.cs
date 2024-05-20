using Avalonia.Media.Imaging;
using System.IO;
using System.Text;
using System;
namespace Tubes3.Models
{
    public static class ImageProcessor
    {
        public static string ConvertImageToBinary(Bitmap bitmap) // Gunakan Avalonia.Media.Imaging.Bitmap
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

        // Function to convert binary to decimal
        public static int binaryToDecimal(string n)
        {
            string num = n;

            // Stores the decimal value
            int dec_value = 0;

            // Initializing base value to 1
            int base1 = 1;

            int len = num.Length;
            for (int i = len - 1; i >= 0; i--)
            {

                // If the current bit is 1
                if (num[i] == '1')
                    dec_value += base1;
                base1 = base1 * 2;
            }

            // Return answer
            return dec_value;
        }

        // Function to convert binary to ASCII
        public static string setStringtoASCII(string str)
        {

            // To store size of s
            int N = (str.Length);

            // If given String is not a
            // valid String
            if (N % 8 != 0)
            {
                return "Not Possible!";
            }

            // To store final answer
            string res = "";

            // Loop to iterate through String
            for (int i = 0; i < N; i += 8)
            {
                int decimal_value
                    = binaryToDecimal((str.Substring(i, 8)));

                // Apprend the ASCII character
                // equivalent to current value
                res += (char)(decimal_value);
            }

            // Return Answer
            return res;
        }

    }
}
