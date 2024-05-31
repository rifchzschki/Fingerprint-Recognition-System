using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
    public static bool KMPSearch(string text, string pattern)
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
                return true;
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
        return false;
    }

    // Fungsi driver utama untuk multithreading, map isinya adalah dictionary yang berisi ascii sama tuple (path, nama orangnya)
    public static string? FindPatternInTexts(string pattern, Dictionary<string, (string, string?)> map)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var tasks = new List<Task<string?>>();

        foreach (var text in map.Keys)
        {
            tasks.Add(Task.Run(() =>
            {
                if (KMPSearch(text, pattern))
                {
                    cancellationTokenSource.Cancel();
                    return text;
                }
                return null;
            }, cancellationTokenSource.Token));
        }

        try
        {
            Task.WaitAny([.. tasks], cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
        }

        foreach (var task in tasks)
        {
            if (task.IsCompletedSuccessfully && task.Result != null)
            {
                return map[task.Result].Item2;
            }
        }

        return null;
    }
}
