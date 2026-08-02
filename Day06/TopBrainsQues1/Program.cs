using System;
using System.Collections.Generic;
using System.Text;

namespace MahirlandAlphabetsandVowels
{
    class Solution
    {
        static bool IsVowel(char ch)
        {
            ch = char.ToLower(ch);
            return ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u';
        }

        static void Main(string[] args)
        {
            string first = Console.ReadLine();
            string second = Console.ReadLine();

            // Store all characters of second word (case-insensitive)
            HashSet<char> secondChars = new HashSet<char>();
            foreach (char c in second)
            {
                secondChars.Add(char.ToLower(c));
            }

            // Step 1: Remove common consonants
            StringBuilder temp = new StringBuilder();

            foreach (char c in first)
            {
                char lower = char.ToLower(c);

                if (!IsVowel(c) && secondChars.Contains(lower))
                    continue;

                temp.Append(c);
            }

            // Step 2: Remove consecutive duplicate characters
            StringBuilder result = new StringBuilder();

            foreach (char c in temp.ToString())
            {
                if (result.Length == 0 || result[result.Length - 1] != c)
                {
                    result.Append(c);
                }
            }

            Console.WriteLine(result.ToString());
        }
    }
}