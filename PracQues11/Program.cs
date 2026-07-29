using System;

namespace PracQues11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double price;
            int quantity;
            double discountPercentage;

            // -------------------------
            // PRICE VALIDATION
            // -------------------------
            while (true)
            {
                Console.Write("Enter item price: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out price))
                {
                    if (double.IsFinite(price) && price >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Price cannot be negative."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric price."
                    );
                }
            }


            // -------------------------
            // QUANTITY VALIDATION
            // -------------------------
            while (true)
            {
                Console.Write("Enter quantity: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out quantity))
                {
                    if (quantity >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Quantity cannot be negative."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid whole number."
                    );
                }
            }


            // -------------------------
            // DISCOUNT VALIDATION
            // -------------------------
            while (true)
            {
                Console.Write(
                    "Enter discount percentage: "
                );

                string? input = Console.ReadLine();

                if (double.TryParse(
                    input,
                    out discountPercentage))
                {
                    if (double.IsFinite(discountPercentage) &&
                        discountPercentage >= 0 &&
                        discountPercentage <= 100)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Discount must be between 0 and 100."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid discount percentage."
                    );
                }
            }


            // -------------------------
            // CALCULATIONS
            // -------------------------

            double subtotal =
                price * quantity;

            double discountAmount =
                subtotal *
                discountPercentage /
                100;

            double finalPayableAmount =
                subtotal -
                discountAmount;


            // -------------------------
            // ROUNDING
            // -------------------------

            subtotal =
                Math.Round(subtotal, 2);

            discountAmount =
                Math.Round(discountAmount, 2);

            finalPayableAmount =
                Math.Round(finalPayableAmount, 2);


            // -------------------------
            // DISPLAY RESULT
            // -------------------------

            Console.WriteLine(
                "\n--- BILL SUMMARY ---"
            );

            Console.WriteLine(
                $"Price: £{price:F2}"
            );

            Console.WriteLine(
                $"Quantity: {quantity}"
            );

            Console.WriteLine(
                $"Discount: {discountPercentage:F2}%"
            );

            Console.WriteLine(
                $"Subtotal: £{subtotal:F2}"
            );

            Console.WriteLine(
                $"Discount Amount: £{discountAmount:F2}"
            );

            Console.WriteLine(
                $"Final Payable Amount: £{finalPayableAmount:F2}"
            );
        }
    }
}