using System;

namespace PracQues10
{
    // Interface
    public interface IInvestmentCalculator
    {
        double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration
        );
    }


    // Simple Investment
    public class SimpleInvestment : IInvestmentCalculator
    {
        public double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration)
        {
            double interest =
                principal *
                (annualRate / 100) *
                duration;

            return principal + interest;
        }
    }


    // Fixed Deposit
    public class FixedDeposit : IInvestmentCalculator
    {
        public double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration)
        {
            double rate =
                annualRate / 100;

            return principal *
                   Math.Pow(
                       1 + rate,
                       duration
                   );
        }
    }


    // Market Investment
    public class MarketInvestment : IInvestmentCalculator
    {
        public double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration)
        {
            double rate =
                annualRate / 100;

            double growth =
                Math.Pow(
                    1 + rate,
                    duration
                );

            return principal * growth;
        }
    }


    // Investment Calculator
    public class InvestmentCalculator
    {
        private IInvestmentCalculator investmentCalculator;

        public InvestmentCalculator(
            IInvestmentCalculator investmentCalculator)
        {
            this.investmentCalculator =
                investmentCalculator;
        }

        public double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration)
        {
            return investmentCalculator
                .CalculateProjectedValue(
                    principal,
                    annualRate,
                    duration
                );
        }
    }


    // Main Program
    public class Program
    {
        public static void Main(string[] args)
        {
            string investmentType;

            double principal;
            double annualRate;

            int duration;
            int choice;


            // Investment Type Validation
            while (true)
            {
                Console.WriteLine(
                    "Enter Investment Type:"
                );

                Console.WriteLine(
                    "1. Simple Investment"
                );

                Console.WriteLine(
                    "2. Fixed Deposit"
                );

                Console.WriteLine(
                    "3. Market Investment"
                );

                Console.Write(
                    "Enter your choice (1, 2 or 3): "
                );

                string? input =
                    Console.ReadLine();

                if (int.TryParse(
                    input,
                    out choice))
                {
                    if (choice == 1)
                    {
                        investmentType =
                            "Simple Investment";

                        break;
                    }
                    else if (choice == 2)
                    {
                        investmentType =
                            "Fixed Deposit";

                        break;
                    }
                    else if (choice == 3)
                    {
                        investmentType =
                            "Market Investment";

                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Please enter 1, 2 or 3."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid number."
                    );
                }
            }


            // Principal Validation
            while (true)
            {
                Console.Write(
                    "Enter Principal Amount (£): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out principal))
                {
                    if (double.IsFinite(principal) &&
                        principal > 0 &&
                        principal <= 10000000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Principal must be greater than £0 and not exceed £10,000,000."
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


            // Annual Rate Validation
            while (true)
            {
                Console.Write(
                    "Enter Annual Rate (%): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out annualRate))
                {
                    if (double.IsFinite(annualRate) &&
                        annualRate >= 0 &&
                        annualRate <= 100)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Annual rate must be between 0% and 100%."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric percentage."
                    );
                }
            }


            // Duration Validation
            while (true)
            {
                Console.Write(
                    "Enter Duration (Years): "
                );

                string? input =
                    Console.ReadLine();

                if (int.TryParse(
                    input,
                    out duration))
                {
                    if (duration > 0 &&
                        duration <= 100)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Duration must be between 1 and 100 years."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid whole number of years."
                    );
                }
            }


            // Select Investment Strategy
            IInvestmentCalculator calculatorStrategy;

            if (investmentType ==
                "Simple Investment")
            {
                calculatorStrategy =
                    new SimpleInvestment();
            }
            else if (investmentType ==
                     "Fixed Deposit")
            {
                calculatorStrategy =
                    new FixedDeposit();
            }
            else
            {
                calculatorStrategy =
                    new MarketInvestment();
            }


            // Create Investment Calculator
            InvestmentCalculator calculator =
                new InvestmentCalculator(
                    calculatorStrategy
                );


            // Calculate Projected Value
            double projectedValue =
                calculator.CalculateProjectedValue(
                    principal,
                    annualRate,
                    duration
                );


            // Validate Calculation Result
            if (!double.IsFinite(projectedValue) ||
                projectedValue < 0)
            {
                Console.WriteLine(
                    "Error: Unable to calculate a valid projected investment value."
                );

                return;
            }


            // Round Result
            projectedValue =
                Math.Round(
                    projectedValue,
                    2
                );


            // Display Result
            Console.WriteLine(
                "\n--- INVESTMENT SUMMARY ---"
            );

            Console.WriteLine(
                $"Investment Type: {investmentType}"
            );

            Console.WriteLine(
                $"Principal Amount: £{principal:F2}"
            );

            Console.WriteLine(
                $"Annual Rate: {annualRate:F2}%"
            );

            Console.WriteLine(
                $"Duration: {duration} years"
            );

            Console.WriteLine(
                $"Projected Investment Value: £{projectedValue:F2}"
            );
        }
    }
}