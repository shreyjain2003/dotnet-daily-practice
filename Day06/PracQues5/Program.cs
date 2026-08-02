using System;

namespace FoodDeliveryTimeEstimator
{
    public class Delivery
    {
        public double Distance;
        public string Weather;
        public int CurrentHour;
        public int PrepTime;

        public Delivery(double distance, string weather, int currentHour, int prepTime)
        {
            Distance = distance;
            Weather = weather;
            CurrentHour = currentHour;
            PrepTime = prepTime;
        }

        public double EstimateTime()
        {
            double totalTime = 30; // Base time

            // Distance
            if (Distance > 5)
            {
                totalTime += (Distance - 5) * 2;
            }

            // Weather
            switch (Weather.ToUpper())
            {
                case "R":
                    totalTime += 10;
                    break;

                case "S":
                    totalTime += 20;
                    break;

                case "C":
                    break;

                default:
                    Console.WriteLine("Invalid Weather Condition");
                    return 0;
            }

            // Rush Hour (5 PM - 8 PM)
            if (CurrentHour >= 17 && CurrentHour <= 20)
            {
                totalTime += 15;
            }

            // Restaurant Preparation Time
            totalTime += PrepTime;

            return totalTime;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Distance (km): ");
            double distance = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Weather (R/S/C): ");
            string weather = Console.ReadLine();

            Console.Write("Enter Current Hour (24-hour format): ");
            int currentHour = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Preparation Time (minutes): ");
            int prepTime = Convert.ToInt32(Console.ReadLine());

            Delivery delivery = new Delivery(distance, weather, currentHour, prepTime);

            Console.WriteLine("\n----- Delivery Estimate -----");
            Console.WriteLine($"Estimated Delivery Time : {delivery.EstimateTime()} minutes");
        }
    }
}