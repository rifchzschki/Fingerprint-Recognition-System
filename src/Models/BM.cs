using System;

/// <summary>The BM class contains the Boyer-Moore string search algorithm.</summary>
public class BM {
    /// <summary>The value of NO_OF_CHARS is 256.</summary>
    static int NO_OF_CHARS = 256;
 
    /// <summary>The max method returns the maximum of two integers.</summary>
    /// <returns>The greater of the two integers.</returns>
    static int Max(int a, int b) { return (a > b) ? a : b; }
 
    /// <summary> The badCharHeuristic method preprocesses the pattern string and creates a bad character array.</summary>
    /// <returns>badchar array</returns>
    static void BadCharHeuristic(char[] str, int size, int[] badchar)
    {
        int i;
 
        // Initialize all occurrences as -1
        for (i = 0; i < NO_OF_CHARS; i++) {
            badchar[i] = -1;
        }
 
        // Fill the actual value of last occurrence of a character
        for (i = 0; i < size; i++) {
            badchar[(int)str[i]] = i;
        }
    }
 
    /// <summary>The Search method takes a series of parameters to specify the search criterion and returns a dataset containing the result set. </summary>
    /// <returns>A DataSet instance containing the matching rows. It contains a maximum number of rows specified by the maxRows parameter</returns>
    static void BMSearch(char[] text, char[] pattern)
    {
        int m = pattern.Length;
        int n = text.Length;
        
        int[] badchar = new int[NO_OF_CHARS];
 
        /* Fill the bad character array by calling badCharHeuristic() */
        BadCharHeuristic(pattern, m, badchar);
 
        int s = 0; // s adalah pergeseran pola terhadap teks
        while (s <= (n - m)) {
            int j = m - 1;
 
            // ngurangin index j selama karakter pola dan teks cocok
            while (j >= 0 && pattern[j] == text[s + j])
                j--;
 
            // jika j = -1, maka pola ditemukan
            if (j < 0) {
                Console.WriteLine("Patterns occur at shift = " + s);
 
                // Geser pola agar karakter selanjutnya di teks sejajar dengan kemunculan terakhirnya di pola
                // Kondisi s+m < n diperlukan untuk kasus ketika pola terjadi di akhir teks
                s += (s + m < n) ? m - badchar[text[s + m]] : 1;
            }
 
            else
                // Geser pola agar badchar di teks sejajar dengan kemunculan terakhirnya di pola
                // Fungsi Max digunakan untuk memastikan bahwa kita mendapatkan pergeseran positif
                // Kita mungkin mendapatkan pergeseran negatif jika kemunculan terakhir karakter buruk di pola berada di sebelah kanan current char
                s += Max(1, j - badchar[text[s + j]]);
        }
    }
 
    // Driver program to testing 
    // public static void Main()
    // {
    //     char[] txt = "ABAAABCD".ToCharArray();
    //     char[] pat = "ABC".ToCharArray();
    //     search(txt, pat);
    // }
}