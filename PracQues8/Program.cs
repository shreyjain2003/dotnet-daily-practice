using System;
namespace PracQues8
{
    public interface IShippingStrategy
    {
        double CalculateShippingCost(double weight, double distance);
    }

    public class StandardShipping : IShippingStrategy
    {
        public double CalculateShippingCost(double weight, double distance)
        {
            double baseCost = 50;
            double weightCost = weight * 10;

            double distanceCost = distance * 0.50;
            return baseCost + weightCost + distanceCost;
        }
    }

    public class ExpressShipping : IShippingStrategy
    {
        public double CalculateShippingCost(double weight, double distance)
        {
            double baseCost = 100;
            double weightCost = weight * 15;
            double distanceCost = distance * 1.00;

            return baseCost + weightCost + distanceCost;
        }
    }

    public class FragileShipping : IShippingStrategy
    {
        public double CalculateShippingCost(double weight, double distance)
        {
            double baseCost = 75;
            double weightCost = weight * 12;
            double distanceCost = distance * 0.75;
            double fragileHandlingCharges = 50;
            
            return baseCost + weightCost + distanceCost + fragileHandlingCharges;
        }
    }

    public class ShippingCalculator
    {
        private IShippingStrategy shippingStrategy;
        public ShippingCalculator(IShippingStrategy shippingStrategy)
        {
            this.shippingStrategy = shippingStrategy;
        }
        public double CalculateShippingCost(double weight, double distance)
        {
            return shippingStrategy.CalculateShippingCost(weight,distance);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            string packageType;
            double weight;
            double distance;
            int choice;

            while(true)
            {
                Console.WriteLine("Enter Package type: ");
                Console.WriteLine("1. Standard\n2.Express\n3.Fragile");
                string? input = Console.ReadLine();

                if(int.TryParse(input, out choice))
                {
                    if(choice == 1)
                    {
                        packageType = "Standard";
                        break;
                    }
                    else if(choice == 2)
                    {
                        packageType = "Express";   
                        break;
                    }
                    else if(choice == 3)
                    {
                        packageType = "Fragile";
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Please enter 1, 2 or 3.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: please enter valid number.");
                }
            }
            while(true)
            {
                Console.WriteLine("Enter Package Weight: ");
                string? input = Console.ReadLine();
                if(double.TryParse(input, out weight))
                {
                    if(double.IsFinite(weight) && weight > 0 && weight <= 1000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Weight must be greater than 0 and noit exceed 1000 kg.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid numeric weight.");
                }
            }
            while(true)
            {
                Console.WriteLine("Enter Shipping Distance(Km): ");
                string? input = Console.ReadLine();

                if(double.TryParse(input, out distance))
                {
                    if(double.IsFinite(distance) && distance > 0 && distance <= 10000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Distance must be greater than 0 and less than 10000 km.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid numeric distance");
                }
            }
            IShippingStrategy shippingStrategy;
            if(packageType == "Standard")
            {
                shippingStrategy = new StandardShipping();
            }
            else if(packageType =="Express")
            {
                shippingStrategy = new ExpressShipping();
            }
            else
            {
                shippingStrategy = new FragileShipping();
            }

            ShippingCalculator calculator = new ShippingCalculator(shippingStrategy);
            double shippingCost = calculator.CalculateShippingCost(weight, distance);
            if(!double.IsFinite(shippingCost) || shippingCost < 0)
            {
                Console.WriteLine("Error: Unable to calculate a valid shipping cost.");
                return;
            }

            shippingCost = Math.Round(shippingCost,2);

            Console.WriteLine("---SHIPPING DETAILS---");
            Console.WriteLine($"Package Type: {packageType}");
            Console.WriteLine($"Weight: {weight:F2} kg");
            Console.WriteLine($"Distance: {distance:F2} km");
            Console.WriteLine($"Shipping Cost: Rs.{shippingCost:F2}");
        }
    }
}