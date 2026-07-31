using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace StringFormat
{
    public record Student(string Name, int Score);

    public class Solution
    {
        public static string GetStudentsJson(string[] items, int minScore)
        {
            List<Student> students = new List<Student>();

            foreach(string item in items)
            {
                string[] parts = item.Split(":");

                string name = parts[0];
                int score = int.Parse(parts[1]);

                students.Add(new Student(name, score));
            }

            List<Student> result = students.Where(student => student.Score >= minScore)
                                            .OrderByDescending(student => student.Score)
                                            .ThenBy(student => student.Name)
                                            .ToList();
            return JsonSerializer.Serialize(result);
        }
        public static void Main(string[] args)
        {
            string[] items =
            {
                "Shrey:85",
                "Sahaj:88",
                "Mayank:75",
                "Khushi:90",
                "Dhruv:45",
                "Shobhit:70"
            };
            int minScore = 72;
            string json = GetStudentsJson(items,minScore);
            Console.WriteLine(json);
        }
    }
}

