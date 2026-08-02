using System;

namespace CarbonFootprintCalculator
{
    class CarbonCalculator
    {
        public string TransportMode;
        public double Distance;
        public string DietType;

        public CarbonCalculator(string transportMode, double distance, string dietType)
        {
            TransportMode = transportMode;
            Distance = distance;
            DietType = dietType;
        }

        public double CalculateFootprint()
        {
            double carbon = 0;

            // Transportation
            switch (TransportMode.ToUpper())
            {
                case "C": // Car
                    carbon = Distance * 0.2;
                    break;

                case "B": // Bus
                    carbon = Distance * 0.05;
                    break;

                case "T": // Train
                    carbon = Distance * 0.03;
                    break;

                case "W": // Walking/Bicycle
                    carbon = 0;
                    break;

                default:
                    Console.WriteLine("Invalid Transportation Mode!");
                    return -1;
            }

            // Electricity usage
            carbon += 2;

            // Diet
            if (DietType.ToUpper() == "N")
                carbon += 1.5;
            else if (DietType.ToUpper() == "V")
                carbon += 0.8;
            else
            {
                Console.WriteLine("Invalid Diet Type!");
                return -1;
            }

            return carbon;
        }

        public string GetRating(double carbon)
        {
            if (carbon < 5)
                return "Low";
            else if (carbon <= 10)
                return "Medium";
            else
                return "High";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Carbon Footprint Calculator ===");

            Console.Write("Enter Transportation Mode (C-Car, B-Bus, T-Train, W-Walking/Bicycle): ");
            string mode = Console.ReadLine();

            Console.Write("Enter Daily Distance (km): ");
            double distance = double.Parse(Console.ReadLine());

            Console.Write("Enter Diet Type (V-Vegetarian, N-Non-Vegetarian): ");
            string diet = Console.ReadLine();

            CarbonCalculator calculator = new CarbonCalculator(mode, distance, diet);

            double footprint = calculator.CalculateFootprint();

            if (footprint != -1)
            {
                Console.WriteLine("\n----- Result -----");
                Console.WriteLine($"Daily Carbon Footprint : {footprint:F2} kg CO₂");
                Console.WriteLine($"Environmental Rating   : {calculator.GetRating(footprint)}");
            }
        }
    }
}