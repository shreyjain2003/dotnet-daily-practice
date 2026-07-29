using System;

namespace PracQues12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double weight;
            double height;

            // -------------------------
            // WEIGHT VALIDATION
            // -------------------------
            while (true)
            {
                Console.Write("Enter weight in kg: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out weight))
                {
                    if (double.IsFinite(weight) &&
                        weight > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Weight must be greater than zero."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric weight."
                    );
                }
            }


            // -------------------------
            // HEIGHT VALIDATION
            // -------------------------
            while (true)
            {
                Console.Write("Enter height in meters: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out height))
                {
                    if (double.IsFinite(height) &&
                        height > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Height must be greater than zero."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric height."
                    );
                }
            }


            // -------------------------
            // BMI CALCULATION
            // -------------------------

            double bmi =
                weight /
                (height * height);


            // -------------------------
            // ROUND BMI
            // -------------------------

            bmi =
                Math.Round(bmi, 2);


            // -------------------------
            // BMI CATEGORY
            // -------------------------

            string category;

            if (bmi < 18.5)
            {
                category = "Underweight";
            }
            else if (bmi < 25)
            {
                category = "Normal Weight";
            }
            else if (bmi < 30)
            {
                category = "Overweight";
            }
            else
            {
                category = "Obese";
            }


            // -------------------------
            // DISPLAY RESULT
            // -------------------------

            Console.WriteLine(
                "\n--- BMI RESULT ---"
            );

            Console.WriteLine(
                $"Weight: {weight:F2} kg"
            );

            Console.WriteLine(
                $"Height: {height:F2} m"
            );

            Console.WriteLine(
                $"BMI: {bmi:F2}"
            );

            Console.WriteLine(
                $"Category: {category}"
            );
        }
    }
}