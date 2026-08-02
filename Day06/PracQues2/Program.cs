using System;

namespace LibraryFineCalculator
{
    public class Library
    {
        public string ItemType;
        public int DaysLate;
        public string UserType;

        public Library(string itemType, int daysLate, string userType)
        {
            ItemType = itemType;
            DaysLate = daysLate;
            UserType = userType;
        }

        public double CalculateFine()
        {
            if (DaysLate <= 3)
                return 0;

            double rate = 0;

            switch (ItemType.ToUpper())
            {
                case "B":
                    rate = 0.50;
                    break;

                case "D":
                    rate = 1.00;
                    break;

                case "J":
                    rate = 0.25;
                    break;

                default:
                    Console.WriteLine("Invalid Item Type");
                    return 0;
            }

            // Grace period of 3 days
            double fine = (DaysLate - 3) * rate;

            // Maximum fine cap
            fine = Math.Min(fine, 20);

            // Student discount
            if (UserType.ToUpper() == "S")
            {
                fine -= fine * 0.50;
            }
            else if (UserType.ToUpper() != "R")
            {
                Console.WriteLine("Invalid User Type");
                return 0;
            }

            return fine;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Item Type (B/D/J): ");
            string itemType = Console.ReadLine();

            Console.Write("Enter Days Late: ");
            int daysLate = int.Parse(Console.ReadLine());

            Console.Write("Enter User Type (S/R): ");
            string userType = Console.ReadLine();

            Library library = new Library(itemType, daysLate, userType);

            Console.WriteLine("\n----- Library Fine -----");
            Console.WriteLine($"Item Type : {itemType.ToUpper()}");
            Console.WriteLine($"Days Late : {daysLate}");
            Console.WriteLine($"User Type : {(userType.ToUpper() == "S" ? "Student" : "Regular")}");
            Console.WriteLine($"Fine      : ${library.CalculateFine():F2}");
        }
    }
}