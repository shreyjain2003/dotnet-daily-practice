using System;

namespace TemperatureAlertSystem
{
    class Weather
    {
        public double CurrentTemperature;
        public double PreviousTemperature;

        public Weather(double currentTemperature, double previousTemperature)
        {
            CurrentTemperature = currentTemperature;
            PreviousTemperature = previousTemperature;
        }

        public void DisplayAlert()
        {
            if (CurrentTemperature < 0)
            {
                Console.WriteLine("Freezing Alert! Risk of ice formation.");
            }
            else if (CurrentTemperature <= 10)
            {
                Console.WriteLine("Cold Alert. Wear warm clothing.");
            }
            else if (CurrentTemperature <= 25)
            {
                Console.WriteLine("Comfortable temperature. No alerts.");
            }
            else if (CurrentTemperature <= 35)
            {
                Console.WriteLine("Heat Alert. Stay hydrated.");
            }
            else
            {
                Console.WriteLine("Extreme Heat Warning! Avoid outdoor activities.");
            }

            if (Math.Abs(CurrentTemperature - PreviousTemperature) > 10)
            {
                Console.WriteLine("Rapid temperature change detected!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Current Temperature: ");
            double currentTemperature = double.Parse(Console.ReadLine());

            Console.Write("Enter Previous Temperature: ");
            double previousTemperature = double.Parse(Console.ReadLine());

            Weather weather = new Weather(currentTemperature, previousTemperature);

            Console.WriteLine("\n=== Temperature Alert ===");
            weather.DisplayAlert();
        }
    }
}