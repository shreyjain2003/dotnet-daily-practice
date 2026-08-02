using System;

namespace BMIHealthClassifier
{
    class Person
    {
        public double Weight;
        public double Height;
        public char IsAthlete;

        public Person(double weight, double height, char isAthlete)
        {
            Weight = weight;
            Height = height;
            IsAthlete = char.ToUpper(isAthlete);
        }

        public void CalculateBMI()
        {
            double bmi = Weight / (Height * Height);

            Console.WriteLine($"\nBMI: {bmi:F2}");

            if (bmi < 18.5)
            {
                Console.WriteLine("Health Status: Underweight");
                Console.WriteLine("Recommendation: Gain weight to reach the normal BMI range.");
            }
            else if (bmi < 25)
            {
                Console.WriteLine("Health Status: Normal");
                Console.WriteLine("Recommendation: Maintain your current weight.");
            }
            else if (bmi < 30)
            {
                Console.WriteLine("Health Status: Overweight");
                Console.WriteLine("Recommendation: Lose weight to reach the normal BMI range.");
            }
            else
            {
                Console.WriteLine("Health Status: Obese");
                Console.WriteLine("Recommendation: Weight loss is recommended.");
            }

            if (IsAthlete == 'Y')
            {
                Console.WriteLine("Note: BMI may not accurately reflect body fat for athletes due to higher muscle mass.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Weight (kg): ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Enter Height (m): ");
            double height = double.Parse(Console.ReadLine());

            Console.Write("Is Athlete? (Y/N): ");
            char isAthlete = char.Parse(Console.ReadLine());

            Person person = new Person(weight, height, isAthlete);

            Console.WriteLine("\n=== BMI Health Report ===");
            person.CalculateBMI();
        }
    }
}