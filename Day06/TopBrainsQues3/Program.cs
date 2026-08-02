using System;
using System.Text;
using System.Globalization;

class Solution
{
    static void Main()
    {
        string input = Console.ReadLine();

        // Trim extra spaces
        input = input.Trim();

        // Remove consecutive duplicate characters
        StringBuilder sb = new StringBuilder();

        foreach (char c in input)
        {
            if (sb.Length == 0 || sb[sb.Length - 1] != c)
            {
                sb.Append(c);
            }
        }

        // Remove multiple spaces
        string[] words = sb.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Convert to Title Case
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = textInfo.ToTitleCase(words[i].ToLower());
        }

        Console.WriteLine(string.Join(" ", words));
    }
}