using System;

namespace TravelItineraryPlanner
{
    class Trip
    {
        public double Distance;
        public string Mode;
        public int Travelers;

        public Trip(double distance, string mode, int travelers)
        {
            Distance = distance;
            Mode = mode;
            Travelers = travelers;
        }

        public double CalculateTime()
        {
            double hours = 0;

            switch (Mode.ToUpper())
            {
                case "CAR":
                    hours = Distance / 60.0;

                    // 15-minute break every 2 hours of driving
                    int breaks = (int)(hours / 2);
                    hours += breaks * 0.25;
                    break;

                case "TRAIN":
                    hours = (Distance / 100.0) + 1;
                    break;

                case "PLANE":
                    hours = (Distance / 800.0) + 3;
                    break;

                default:
                    Console.WriteLine("Invalid Transport Mode");
                    return 0;
            }

            return hours;
        }

        public double CalculateCost()
        {
            double rate = 0;

            switch (Mode.ToUpper())
            {
                case "CAR":
                    rate = 8;
                    break;

                case "TRAIN":
                    rate = 2;
                    break;

                case "PLANE":
                    rate = 10;
                    break;
            }

            return Distance * rate * Travelers;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Travel Itinerary Planner ===");

            Console.Write("Enter Distance (km): ");
            double distance = double.Parse(Console.ReadLine());

            Console.Write("Enter Transport Mode (Car/Train/Plane): ");
            string mode = Console.ReadLine();

            Console.Write("Enter Number of Travelers: ");
            int travelers = int.Parse(Console.ReadLine());

            Trip trip = new Trip(distance, mode, travelers);

            Console.WriteLine("\n===== RESULT =====");
            Console.WriteLine($"Total Travel Time : {trip.CalculateTime():F2} hours");
            Console.WriteLine($"Estimated Cost    : {trip.CalculateCost():F2}");
        }
    }
}