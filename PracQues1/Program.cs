using System;
namespace PracQues1
{
    public class PracQues1
    {
        public static void Main(string[] args)
        {
            double price;
            int quantity;
            double discountPercentage;

            while (true)
            {
                Console.WriteLine("Enter item's Price: ");
                string? input = Console.ReadLine();
                if (double.TryParse(input, out price))
                {
                    if (price >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Price cannot be negative.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid price.");
                }
            }
            while(true)
            {
                Console.WriteLine("Enter the quantity: ");
                string? input = Console.ReadLine();
                if(int.TryParse(input,out quantity))
                {
                    if(quantity >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: quantity cannot be in negative.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid whole number.");
                }
            }

            while(true)
            {
                Console.WriteLine("Enter discount percentage: ");
                string? input = Console.ReadLine();
                if(double.TryParse(input, out discountPercentage))
                {
                    if(discountPercentage >=0 && discountPercentage <= 100)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Discount must be in between 0 and 100.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid discount percentage.");
                }
            }
            double subtotal = price * quantity;
            double discountAmount = subtotal * discountPercentage / 100;
            double finalAmount = subtotal - discountAmount;

            Console.WriteLine("Bill Summary...");
            Console.WriteLine($"Subtotal= Rs.{Math.Round(subtotal,2):F2}");
            Console.WriteLine($"Discount Amount = Rs.{Math.Round(discountAmount, 2):F2}");
            Console.WriteLine($"Final Payable Amount = Rd.{Math.Round(finalAmount,2):F2}");

        }   
    }

}