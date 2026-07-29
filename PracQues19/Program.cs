using System;

namespace PracQues19
{
    // =========================================
    // PATIENT CLASS
    // =========================================

    public class Patient
    {
        public int Age { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double Temperature { get; set; }


        // Constructor
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
    }


    // =========================================
    // PATIENT VALIDATOR CLASS
    // =========================================

    public class PatientValidator
    {
        // -----------------------------------------
        // VALIDATE AGE
        // -----------------------------------------

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

            age = 0;
            return false;
        }


        // -----------------------------------------
        // VALIDATE WEIGHT
        // -----------------------------------------

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

            weight = 0;
            return false;
        }


        // -----------------------------------------
        // VALIDATE HEIGHT
        // -----------------------------------------

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

            height = 0;
            return false;
        }


        // -----------------------------------------
        // VALIDATE TEMPERATURE
        // -----------------------------------------

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

            temperature = 0;
            return false;
        }
    }


    // =========================================
    // PROGRAM CLASS
    // =========================================

    public class Program
    {
        public static void Main(string[] args)
        {
            int age;
            double weight;
            double height;
            double temperature;


            // Create validator object
            PatientValidator validator =
                new PatientValidator();


            // =========================================
            // AGE INPUT
            // =========================================

            while (true)
            {
                Console.Write(
                    "Enter Patient Age: "
                );

                string? input =
                    Console.ReadLine();

                if (validator.ValidateAge(
                    input,
                    out age))
                {
                    break;
                }

                Console.WriteLine(
                    "Error: Please enter a valid age between 1 and 120."
                );
            }


            // =========================================
            // WEIGHT INPUT
            // =========================================

            while (true)
            {
                Console.Write(
                    "Enter Weight (kg): "
                );

                string? input =
                    Console.ReadLine();

                if (validator.ValidateWeight(
                    input,
                    out weight))
                {
                    break;
                }

                Console.WriteLine(
                    "Error: Please enter a valid weight greater than 0 and up to 500 kg."
                );
            }


            // =========================================
            // HEIGHT INPUT
            // =========================================

            while (true)
            {
                Console.Write(
                    "Enter Height (meters): "
                );

                string? input =
                    Console.ReadLine();

                if (validator.ValidateHeight(
                    input,
                    out height))
                {
                    break;
                }

                Console.WriteLine(
                    "Error: Please enter a valid height greater than 0 and up to 3 metres."
                );
            }


            // =========================================
            // TEMPERATURE INPUT
            // =========================================

            while (true)
            {
                Console.Write(
                    "Enter Body Temperature (°C): "
                );

                string? input =
                    Console.ReadLine();

                if (validator.ValidateTemperature(
                    input,
                    out temperature))
                {
                    break;
                }

                Console.WriteLine(
                    "Error: Please enter a valid temperature between 25°C and 45°C."
                );
            }


            // =========================================
            // CREATE PATIENT OBJECT
            // =========================================

            Patient patient =
                new Patient(
                    age,
                    weight,
                    height,
                    temperature
                );


            // =========================================
            // CALCULATE BMI
            // =========================================

            double bmi =
                patient.CalculateBMI();

            bmi =
                Math.Round(
                    bmi,
                    2
                );


            // =========================================
            // DISPLAY PATIENT SUMMARY
            // =========================================

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
                $"Temperature: {patient.Temperature:F2} °C"
            );

            Console.WriteLine(
                $"BMI: {bmi:F2}"
            );
        }
    }
}