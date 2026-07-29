using System;

namespace PracQues9
{
    // Patient Class
    public class Patient
    {
        public int Age;
        public double Weight;
        public double Height;
        public double Temperature;

        public Patient(
            int age,
            double weight,
            double height,
            double temperature)
        {
            Age = age;
            Weight = weight;
            Height = height;
            Temperature = temperature;
        }

        // Calculate BMI
        public double CalculateBMI()
        {
            return Weight /
                   (Height * Height);
        }

        // Get BMI Category
        public string GetBMICategory()
        {
            double bmi = CalculateBMI();

            if (bmi < 18.5)
            {
                return "Underweight";
            }
            else if (bmi < 25)
            {
                return "Normal Weight";
            }
            else if (bmi < 30)
            {
                return "Overweight";
            }
            else
            {
                return "Obese";
            }
        }
    }


    // Validation Class
    public class Validation
    {
        // Validate Age
        public bool ValidateAge(
            string? input,
            out int age)
        {
            if (int.TryParse(
                input,
                out age))
            {
                if (age > 0 &&
                    age <= 120)
                {
                    return true;
                }
            }

            return false;
        }


        // Validate Weight
        public bool ValidateWeight(
            string? input,
            out double weight)
        {
            if (double.TryParse(
                input,
                out weight))
            {
                if (double.IsFinite(weight) &&
                    weight > 0 &&
                    weight <= 500)
                {
                    return true;
                }
            }

            return false;
        }


        // Validate Height
        public bool ValidateHeight(
            string? input,
            out double height)
        {
            if (double.TryParse(
                input,
                out height))
            {
                if (double.IsFinite(height) &&
                    height > 0 &&
                    height <= 3)
                {
                    return true;
                }
            }

            return false;
        }


        // Validate Temperature
        public bool ValidateTemperature(
            string? input,
            out double temperature)
        {
            if (double.TryParse(
                input,
                out temperature))
            {
                if (double.IsFinite(temperature) &&
                    temperature >= 25 &&
                    temperature <= 45)
                {
                    return true;
                }
            }

            return false;
        }
    }


    // Main Program
    public class Program
    {
        public static void Main(string[] args)
        {
            int age;
            double weight;
            double height;
            double temperature;

            Validation validation =
                new Validation();


            // Age Input
            while (true)
            {
                Console.Write(
                    "Enter Patient Age: "
                );

                string? input =
                    Console.ReadLine();

                if (validation.ValidateAge(
                    input,
                    out age))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid age between 1 and 120."
                    );
                }
            }


            // Weight Input
            while (true)
            {
                Console.Write(
                    "Enter Patient Weight (kg): "
                );

                string? input =
                    Console.ReadLine();

                if (validation.ValidateWeight(
                    input,
                    out weight))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid weight between 0 and 500 kg."
                    );
                }
            }


            // Height Input
            while (true)
            {
                Console.Write(
                    "Enter Patient Height (m): "
                );

                string? input =
                    Console.ReadLine();

                if (validation.ValidateHeight(
                    input,
                    out height))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid height between 0 and 3 meters."
                    );
                }
            }


            // Temperature Input
            while (true)
            {
                Console.Write(
                    "Enter Body Temperature (°C): "
                );

                string? input =
                    Console.ReadLine();

                if (validation.ValidateTemperature(
                    input,
                    out temperature))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid temperature between 25°C and 45°C."
                    );
                }
            }


            // Create Patient Object
            Patient patient =
                new Patient(
                    age,
                    weight,
                    height,
                    temperature
                );


            // Calculate BMI
            double bmi =
                Math.Round(
                    patient.CalculateBMI(),
                    2
                );


            // Display Patient Summary
            Console.WriteLine(
                "\n--- PATIENT SUMMARY ---"
            );

            Console.WriteLine(
                $"Age: {patient.Age} years"
            );

            Console.WriteLine(
                $"Weight: {patient.Weight:F2} kg"
            );

            Console.WriteLine(
                $"Height: {patient.Height:F2} m"
            );

            Console.WriteLine(
                $"Body Temperature: {patient.Temperature:F2} °C"
            );

            Console.WriteLine(
                $"BMI: {bmi:F2}"
            );

            Console.WriteLine(
                $"BMI Category: {patient.GetBMICategory()}"
            );
        }
    }
}