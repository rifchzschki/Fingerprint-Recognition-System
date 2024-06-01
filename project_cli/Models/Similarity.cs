
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Tubes3.Models
{
    public class Similarity
    {
        // Fungsi untuk menghitung panjang LCS (Longest Common Subsequence)
        public static int LcsLength(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            return dp[m, n];
        }

        // Fungsi untuk menghitung skor similaritas antara dua string
        public static double SimilarityScore(string s1, string s2)
        {
            int lcsLen = LcsLength(s1, s2);
            int maxLen = Math.Max(s1.Length, s2.Length);
            return maxLen == 0 ? 1.0 : (double)lcsLen / maxLen;
        }

        // Fungsi untuk menemukan pasangan dengan similaritas tertinggi, candidates isinya ascii representation dan valuenya path dan namanya.
        public static (double, string) FindBestMatch(string target, Dictionary<string, (string, string)> candidates)
        {
            var tasks = candidates.Select(candidate =>
            {
                return Task.Run(() =>
                {
                    var asciiRepresentation = candidate.Key;
                    var (path, name) = candidate.Value;
                    var similarity = SimilarityScore(target, asciiRepresentation);
                    return (similarity, path);
                });
            });

            var results = tasks.Select(t => t.Result);
            var bestMatch = results.OrderByDescending(result => result.Item1).First();
            return bestMatch;
        }
    }
}