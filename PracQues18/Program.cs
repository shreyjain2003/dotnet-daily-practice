using System;

namespace PracQues18
{
    // =========================================
    // INTERFACE
    // =========================================

    public interface IShippingStrategy
    {
        double CalculateShippingCost(
            double weight,
            double distance
        );
    }


    // =========================================
    // STANDARD SHIPPING
    // =========================================

    public class StandardShipping : IShippingStrategy
    {
        public double CalculateShippingCost(
            double weight,
            double distance)
        {
            double baseCost = 50;
            double weightCost = weight * 10;
            double distanceCost = distance * 0.50;

            return baseCost +
                   weightCost +
                   distanceCost;
        }
    }


    // =========================================
    // EXPRESS SHIPPING
    // =========================================

    public class ExpressShipping : IShippingStrategy
    {
        public double CalculateShippingCost(
            double weight,
            double distance)
        {
            double baseCost = 100;
            double weightCost = weight * 15;
            double distanceCost = distance * 1.00;

            return baseCost +
                   weightCost +
                   distanceCost;
        }
    }


    // =========================================
    // FRAGILE SHIPPING
    // =========================================

    public class FragileShipping : IShippingStrategy
    {
        public double CalculateShippingCost(
            double weight,
            double distance)
        {
            double baseCost = 75;
            double weightCost = weight * 12;
            double distanceCost = distance * 0.75;
            double fragileHandlingCharge = 50;

            return baseCost +
                   weightCost +
                   distanceCost +
                   fragileHandlingCharge;
        }
    }


    // =========================================
    // SHIPPING CALCULATOR
    // =========================================

    public class ShippingCalculator
    {
        private readonly IShippingStrategy shippingStrategy;

        public ShippingCalculator(
            IShippingStrategy shippingStrategy)
        {
            this.shippingStrategy =
                shippingStrategy;
        }

        public double CalculateShippingCost(
            double weight,
            double distance)
        {
            return shippingStrategy
                .CalculateShippingCost(
                    weight,
                    distance
                );
        }
    }


    // =========================================
    // PROGRAM
    // =========================================

    public class Program
    {
        public static void Main(string[] args)
        {
            string packageType;
            double weight;
            double distance;
            int choice;


            // =========================================
            // PACKAGE TYPE
            // =========================================

            while (true)
            {
                Console.WriteLine(
                    "\nSelect Package Type:"
                );

                Console.WriteLine(
                    "1. Standard"
                );

                Console.WriteLine(
                    "2. Express"
                );

                Console.WriteLine(
                    "3. Fragile"
                );

                Console.Write(
                    "Enter your choice: "
                );

                string? input =
                    Console.ReadLine();

                if (int.TryParse(
                    input,
                    out choice))
                {
                    if (choice == 1)
                    {
                        packageType =
                            "Standard";

                        break;
                    }
                    else if (choice == 2)
                    {
                        packageType =
                            "Express";

                        break;
                    }
                    else if (choice == 3)
                    {
                        packageType =
                            "Fragile";

                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Please enter 1, 2, or 3."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric choice."
                    );
                }
            }


            // =========================================
            // WEIGHT VALIDATION
            // =========================================

            while (true)
            {
                Console.Write(
                    "\nEnter Package Weight (kg): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out weight))
                {
                    if (double.IsFinite(weight) &&
                        weight > 0 &&
                        weight <= 1000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Weight must be greater than 0 and not exceed 1000 kg."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric weight."
                    );
                }
            }


            // =========================================
            // DISTANCE VALIDATION
            // =========================================

            while (true)
            {
                Console.Write(
                    "\nEnter Shipping Distance (km): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out distance))
                {
                    if (double.IsFinite(distance) &&
                        distance > 0 &&
                        distance <= 10000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Distance must be greater than 0 and not exceed 10000 km."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric distance."
                    );
                }
            }


            // =========================================
            // SELECT SHIPPING STRATEGY
            // =========================================

            IShippingStrategy shippingStrategy;

            if (packageType == "Standard")
            {
                shippingStrategy =
                    new StandardShipping();
            }
            else if (packageType == "Express")
            {
                shippingStrategy =
                    new ExpressShipping();
            }
            else
            {
                shippingStrategy =
                    new FragileShipping();
            }


            // =========================================
            // CREATE CALCULATOR
            // =========================================

            ShippingCalculator calculator =
                new ShippingCalculator(
                    shippingStrategy
                );


            // =========================================
            // CALCULATE SHIPPING COST
            // =========================================

            double shippingCost =
                calculator.CalculateShippingCost(
                    weight,
                    distance
                );


            // =========================================
            // FINAL SAFETY CHECK
            // =========================================

            if (!double.IsFinite(shippingCost) ||
                shippingCost < 0)
            {
                Console.WriteLine(
                    "Error: Unable to calculate a valid shipping cost."
                );

                return;
            }


            // =========================================
            // ROUND RESULT
            // =========================================

            shippingCost =
                Math.Round(
                    shippingCost,
                    2
                );


            // =========================================
            // DISPLAY RESULT
            // =========================================

            Console.WriteLine(
                "\n--- SHIPPING DETAILS ---"
            );

            Console.WriteLine(
                $"Package Type: {packageType}"
            );

            Console.WriteLine(
                $"Weight: {weight:F2} kg"
            );

            Console.WriteLine(
                $"Distance: {distance:F2} km"
            );

            Console.WriteLine(
                $"Shipping Cost: £{shippingCost:F2}"
            );
        }
    }
}