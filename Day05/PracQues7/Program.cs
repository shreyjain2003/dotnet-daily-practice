using System;
namespace EcommerceDiscountCalculator
{
    public class Customer
    {
        public string Name;
        public double PurchaseAmount;
        public Customer(string name, double purchaseAmount)
        {
            Name = name;
            PurchaseAmount = purchaseAmount;
        }

        public double Discount(string name, double purchaseAmount)
        {
            double discount = 0;

            switch (name.ToUpper())
            {
                case "R":
                    if (purchaseAmount > 100)
                        discount = purchaseAmount * 5 / 100;
                    break;

                case "P":
                    discount = purchaseAmount * 10 / 100;
                    break;

                case "V":
                    discount = purchaseAmount * 15 / 100;
                    if (purchaseAmount > 200)
                        discount += purchaseAmount * 5 / 100;
                    break;
            }

            return discount;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Enter Your Category ('R'-> Regular, 'P'-> Premium, 'V'-> VIP):");
            string category = Console.ReadLine();

            Console.WriteLine("Enter your purchase amount: ");
            double purchaseAmount = double.Parse(Console.ReadLine());
            Customer customer = new Customer(category, purchaseAmount);
            double discount = customer.Discount(category, purchaseAmount);

            double PaybleAmount = purchaseAmount - discount;
            Console.WriteLine("----Bill----");
            Console.WriteLine($"Original Amount: {purchaseAmount}");
            Console.WriteLine($"Discount Amount: {discount}");
            Console.WriteLine($"Final Price: {PaybleAmount}");
        }
    }
}