using System;
using System.Linq;
namespace PracQues5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[] marks = new double[5];

            for(int i = 0;i < marks.Length; i++)
            {
                while(true)
                {
                    Console.WriteLine("Enter the marks of subject "+(i+1));
                    string? input = Console.ReadLine();

                    if(double.TryParse(input, out marks[i]))
                    {
                        if(marks[i] >= 0 && marks[i] <= 100)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error: Marks must be between 0 and 100");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Please enter a valid numeric mark.");
                    }
                }
            }
            double total = marks.Sum();
            double average = total / 5;
            double percentage = Math.Round(((total / 500) * 100),2);

            Console.WriteLine("Student Performance...");
            Console.WriteLine($"Total: {total}");
            Console.WriteLine($"Average: {average}");
            Console.WriteLine($"Percentage: {percentage} %");
        }
    }
}