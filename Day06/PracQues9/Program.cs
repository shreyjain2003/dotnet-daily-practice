using System;

namespace SmartHomeEnergyMonitor
{
    class EnergyMonitor
    {
        public double LightHours;
        public double ACHours;
        public double TVHours;

        public EnergyMonitor(double lightHours, double acHours, double tvHours)
        {
            LightHours = lightHours;
            ACHours = acHours;
            TVHours = tvHours;
        }

        public double CalculateCost()
        {
            double lightCost = LightHours * 0.1 * 0.15;
            double acCost = ACHours * 1.5 * 0.15;
            double tvCost = TVHours * 0.3 * 0.15;

            return lightCost + acCost + tvCost;
        }

        public void ShowAlerts()
        {
            Console.WriteLine("\n=== Usage Alerts ===");

            if (LightHours > 10)
                Console.WriteLine("Alert: Lights used for more than 10 hours.");

            if (ACHours > 8)
                Console.WriteLine("Alert: AC used for more than 8 hours.");

            if (TVHours > 5)
                Console.WriteLine("Alert: TV used for more than 5 hours.");
        }

        public void ShowSuggestion(double totalCost)
        {
            Console.WriteLine("\n=== Suggestions ===");

            if (totalCost > 5)
                Console.WriteLine("Energy cost exceeds $5/day. Consider reducing appliance usage.");
            else
                Console.WriteLine("Energy consumption is within the recommended limit.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Smart Home Energy Monitor ===");

            Console.Write("Enter Light Usage (hours): ");
            double lightHours = double.Parse(Console.ReadLine());

            Console.Write("Enter AC Usage (hours): ");
            double acHours = double.Parse(Console.ReadLine());

            Console.Write("Enter TV Usage (hours): ");
            double tvHours = double.Parse(Console.ReadLine());

            EnergyMonitor monitor = new EnergyMonitor(lightHours, acHours, tvHours);

            double totalCost = monitor.CalculateCost();

            Console.WriteLine("\n=== Daily Report ===");
            Console.WriteLine($"Daily Energy Cost: ${totalCost:F2}");

            monitor.ShowAlerts();
            monitor.ShowSuggestion(totalCost);
        }
    }
}