using System;

class KMP
{
    // Fungsi untuk membuat array LPS (Longest Proper Prefix which is also Suffix)
    private static int[] ComputeLPSArray(string pattern)
    {
        int m = pattern.Length;
        int[] lps = new int[m];
        int len = 0;
        int i = 1;
        lps[0] = 0; // lps[0] selalu 0

        while (i < m)
        {
            if (pattern[i] == pattern[len])
            {
                len++;
                lps[i] = len;
                i++;
            }
            else
            {
                if (len != 0)
                {
                    len = lps[len - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }
        return lps;
    }

    // Fungsi utama untuk pencarian string
    public static void KMPSearch(string text, string pattern)
    {
        int n = text.Length;
        int m = pattern.Length;
        int[] lps = ComputeLPSArray(pattern);

        int i = 0; // Indeks untuk text
        int j = 0; // Indeks untuk pattern

        while (i < n)
        {
            if (pattern[j] == text[i])
            {
                j++;
                i++;
            }

            if (j == m) // Kecocokan ditemukan
            {
                Console.WriteLine($"Pola ditemukan pada indeks {i - j}");
                j = lps[j - 1];
            }
            else if (i < n && pattern[j] != text[i])
            {
                if (j != 0)
                {
                    j = lps[j - 1];
                }
                else
                {
                    i++;
                }
            }
        }
    }
}