using System;

namespace PlantWateringScheduler
{
    class Plant
    {
        public string PlantType;
        public string Season;
        public double Humidity;
        public string SoilType;

        public Plant(string plantType, string season, double humidity, string soilType)
        {
            PlantType = plantType;
            Season = season;
            Humidity = humidity;
            SoilType = soilType;
        }

        public double CalculateInterval()
        {
            double interval = 0;

            switch (PlantType.ToUpper())
            {
                case "CACTUS":
                    interval = 14;
                    break;

                case "FERN":
                    interval = 3;
                    break;

                case "ROSE":
                    interval = 5;
                    break;

                case "TOMATO":
                    interval = 2;
                    break;

                default:
                    Console.WriteLine("Invalid Plant Type");
                    return 0;
            }

            // Season Adjustment
            if (Season.ToUpper() == "SUMMER")
                interval *= 0.8;       // Reduce by 20%
            else if (Season.ToUpper() == "WINTER")
                interval *= 1.3;       // Increase by 30%

            // Humidity Adjustment
            if (Humidity > 60)
                interval *= 1.15;      // Increase by 15%

            // Soil Adjustment
            if (SoilType.ToUpper() == "CLAY")
                interval += 1;
            else if (SoilType.ToUpper() == "SANDY")
                interval -= 1;

            if (interval < 1)
                interval = 1;

            return interval;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Plant Watering Scheduler ===");

            Console.Write("Enter Plant Type (Cactus/Fern/Rose/Tomato): ");
            string plantType = Console.ReadLine();

            Console.Write("Enter Season (Summer/Winter/Other): ");
            string season = Console.ReadLine();

            Console.Write("Enter Humidity (%): ");
            double humidity = double.Parse(Console.ReadLine());

            Console.Write("Enter Soil Type (Clay/Sandy/Normal): ");
            string soilType = Console.ReadLine();

            Plant plant = new Plant(plantType, season, humidity, soilType);

            double interval = plant.CalculateInterval();

            Console.WriteLine("\n===== RESULT =====");
            Console.WriteLine($"Watering Interval : {interval:F1} days");
            Console.WriteLine($"Next Watering Date: {DateTime.Today.AddDays(interval):d}");
        }
    }
}