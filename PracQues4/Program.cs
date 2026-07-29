using System;
namespace PracQues4
{
    public class Program
    {
        public class BankOperations
        {
            public double OpeningBalance;
            public double Deposits;
            public double Withdrawals;
            public BankOperations(double openingBalance, double deposits, double withdrawals)
            {
                OpeningBalance = openingBalance;
                Deposits = deposits;
                Withdrawals = withdrawals;
            }
            public double FinalBalance()
            {
                return OpeningBalance + Deposits - Withdrawals;
            }
        }
        public static void Main(string[] args)
        {
            double openingBalance;
            double deposits;
            double withdrawals;
            while (true)
            {
                Console.WriteLine("Enter your Opening Balance of Account: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out openingBalance))
                {
                    if (openingBalance > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Opening Balance cannot be less than zero.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid Opening Balance!");
                }
            }
            double availableBalance = openingBalance;
            while (true)
            {
                Console.WriteLine("Enter Deposite Amount: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out deposits))
                {
                    if (deposits > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Deposits cannot be negative");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter valid amount.");
                }
            }

            while (true)
            {
                Console.WriteLine("Enter the Amount to Withdraw: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out withdrawals))
                {
                    if (withdrawals < 0)
                    {
                        Console.WriteLine(
                            "Error: Withdrawal cannot be negative."
                        );
                    }
                    else if (withdrawals > openingBalance + deposits)
                    {
                        Console.WriteLine(
                            "Error: Withdrawal exceeds available balance."
                        );
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid Amount to Withdraw.");
                }
            }
            BankOperations operations = new BankOperations(openingBalance, deposits, withdrawals);
            double final = operations.FinalBalance();

            Console.WriteLine($"Opening Balance: {openingBalance}");
            Console.WriteLine($"Deposits: {deposits}");
            Console.WriteLine($"Withdrawals: {withdrawals}");
            Console.WriteLine($"Final Amount in Account: {final}");
        }
    }
}