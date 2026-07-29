using System;

namespace PracQues15
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Array to store marks of 5 subjects
            double[] marks = new double[5];


            // -------------------------
            // INPUT AND VALIDATION
            // -------------------------

            for (int i = 0; i < marks.Length; i++)
            {
                while (true)
                {
                    Console.Write(
                        $"Enter marks for Subject {i + 1}: "
                    );

                    string? input =
                        Console.ReadLine();

                    if (double.TryParse(
                        input,
                        out marks[i]))
                    {
                        if (double.IsFinite(marks[i]) &&
                            marks[i] >= 0 &&
                            marks[i] <= 100)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine(
                                "Error: Marks must be between 0 and 100."
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Please enter a valid numeric mark."
                        );
                    }
                }
            }


            // -------------------------
            // CALCULATE TOTAL
            // -------------------------

            double total = 0;

            for (int i = 0; i < marks.Length; i++)
            {
                total += marks[i];
            }


            // -------------------------
            // CALCULATE AVERAGE
            // -------------------------

            double average =
                total / marks.Length;


            // -------------------------
            // CALCULATE PERCENTAGE
            // -------------------------

            double maximumMarks =
                marks.Length * 100;

            double percentage =
                (total / maximumMarks) * 100;


            // -------------------------
            // ROUND VALUES
            // -------------------------

            total =
                Math.Round(total, 2);

            average =
                Math.Round(average, 2);

            percentage =
                Math.Round(percentage, 2);


            // -------------------------
            // DISPLAY RESULT
            // -------------------------

            Console.WriteLine(
                "\n--- STUDENT PERFORMANCE ---"
            );

            Console.WriteLine(
                $"Total Marks: {total:F2}"
            );

            Console.WriteLine(
                $"Average Marks: {average:F2}"
            );

            Console.WriteLine(
                $"Percentage: {percentage:F2}%"
            );
        }
    }
}