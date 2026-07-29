using System;

namespace PracQues14
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double openingBalance;
            double deposits;
            double withdrawals;


            // -------------------------
            // OPENING BALANCE
            // -------------------------

            while (true)
            {
                Console.Write(
                    "Enter Opening Balance (£): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out openingBalance))
                {
                    if (double.IsFinite(openingBalance) &&
                        openingBalance >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Opening balance cannot be negative."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric amount."
                    );
                }
            }


            // -------------------------
            // DEPOSIT VALIDATION
            // -------------------------

            while (true)
            {
                Console.Write(
                    "Enter Total Deposits (£): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out deposits))
                {
                    if (double.IsFinite(deposits) &&
                        deposits >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Deposits cannot be negative."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric amount."
                    );
                }
            }


            // -------------------------
            // CALCULATE AVAILABLE BALANCE
            // -------------------------

            double availableBalance =
                openingBalance +
                deposits;


            // -------------------------
            // WITHDRAWAL VALIDATION
            // -------------------------

            while (true)
            {
                Console.Write(
                    "Enter Total Withdrawals (£): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out withdrawals))
                {
                    if (!double.IsFinite(withdrawals) ||
                        withdrawals < 0)
                    {
                        Console.WriteLine(
                            "Error: Withdrawal cannot be negative."
                        );
                    }
                    else if (withdrawals >
                             availableBalance)
                    {
                        Console.WriteLine(
                            $"Error: Insufficient funds. Available balance is £{availableBalance:F2}."
                        );
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric amount."
                    );
                }
            }


            // -------------------------
            // CALCULATE FINAL BALANCE
            // -------------------------

            double finalBalance =
                openingBalance +
                deposits -
                withdrawals;


            // -------------------------
            // DISPLAY RESULT
            // -------------------------

            Console.WriteLine(
                "\n--- BANK ACCOUNT SUMMARY ---"
            );

            Console.WriteLine(
                $"Opening Balance: £{openingBalance:F2}"
            );

            Console.WriteLine(
                $"Total Deposits: £{deposits:F2}"
            );

            Console.WriteLine(
                $"Total Withdrawals: £{withdrawals:F2}"
            );

            Console.WriteLine(
                $"Final Balance: £{finalBalance:F2}"
            );
        }
    }
}