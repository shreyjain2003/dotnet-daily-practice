using System;

namespace PracQues20
{
    // =========================================
    // INTERFACE
    // =========================================

    public interface IInvestmentCalculator
    {
        double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration
        );
    }


    // =========================================
    // FIXED DEPOSIT CALCULATOR
    // =========================================

    public class FixedDepositCalculator
        : IInvestmentCalculator
    {
        public double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration)
        {
            double interest =
                principal *
                annualRate *
                duration /
                100;

            return principal + interest;
        }
    }


    // =========================================
    // COMPOUND INVESTMENT CALCULATOR
    // =========================================

    public class CompoundInvestmentCalculator
        : IInvestmentCalculator
    {
        public double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration)
        {
            return principal *
                   Math.Pow(
                       1 + annualRate / 100,
                       duration
                   );
        }
    }


    // =========================================
    // HIGH GROWTH INVESTMENT CALCULATOR
    // =========================================

    public class HighGrowthInvestmentCalculator
        : IInvestmentCalculator
    {
        public double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration)
        {
            double adjustedRate =
                annualRate * 1.2;

            return principal *
                   Math.Pow(
                       1 + adjustedRate / 100,
                       duration
                   );
        }
    }


    // =========================================
    // INVESTMENT CALCULATOR SERVICE
    // =========================================

    public class InvestmentCalculatorService
    {
        private readonly IInvestmentCalculator calculator;

        public InvestmentCalculatorService(
            IInvestmentCalculator calculator)
        {
            this.calculator = calculator;
        }

        public double CalculateProjectedValue(
            double principal,
            double annualRate,
            int duration)
        {
            return calculator.CalculateProjectedValue(
                principal,
                annualRate,
                duration
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
            string investmentType;
            double principal;
            double annualRate;
            int duration;
            int choice;


            // =========================================
            // INVESTMENT TYPE
            // =========================================

            while (true)
            {
                Console.WriteLine(
                    "\nSelect Investment Type:"
                );

                Console.WriteLine(
                    "1. Fixed Deposit"
                );

                Console.WriteLine(
                    "2. Compound Investment"
                );

                Console.WriteLine(
                    "3. High Growth Investment"
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
                        investmentType =
                            "Fixed Deposit";

                        break;
                    }
                    else if (choice == 2)
                    {
                        investmentType =
                            "Compound Investment";

                        break;
                    }
                    else if (choice == 3)
                    {
                        investmentType =
                            "High Growth Investment";

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
            // PRINCIPAL VALIDATION
            // =========================================

            while (true)
            {
                Console.Write(
                    "\nEnter Principal Amount (£): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out principal))
                {
                    if (double.IsFinite(principal) &&
                        principal > 0 &&
                        principal <= 1000000000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Principal must be greater than £0 and not exceed £1,000,000,000."
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


            // =========================================
            // ANNUAL RATE VALIDATION
            // =========================================

            while (true)
            {
                Console.Write(
                    "\nEnter Annual Rate (%): "
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


            // =========================================
            // DURATION VALIDATION
            // =========================================

            while (true)
            {
                Console.Write(
                    "\nEnter Duration (Years): "
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
                        "Error: Please enter a valid whole number."
                    );
                }
            }


            // =========================================
            // SELECT INVESTMENT CALCULATOR
            // =========================================

            IInvestmentCalculator calculator;

            if (investmentType ==
                "Fixed Deposit")
            {
                calculator =
                    new FixedDepositCalculator();
            }
            else if (investmentType ==
                     "Compound Investment")
            {
                calculator =
                    new CompoundInvestmentCalculator();
            }
            else
            {
                calculator =
                    new HighGrowthInvestmentCalculator();
            }


            // =========================================
            // CREATE SERVICE
            // =========================================

            InvestmentCalculatorService service =
                new InvestmentCalculatorService(
                    calculator
                );


            // =========================================
            // CALCULATE PROJECTED VALUE
            // =========================================

            double projectedValue =
                service.CalculateProjectedValue(
                    principal,
                    annualRate,
                    duration
                );


            // =========================================
            // FINAL SAFETY CHECK
            // =========================================

            if (!double.IsFinite(projectedValue) ||
                projectedValue < principal)
            {
                Console.WriteLine(
                    "Error: Unable to calculate a valid projected investment value."
                );

                return;
            }


            // =========================================
            // CALCULATE PROFIT
            // =========================================

            double projectedReturn =
                projectedValue -
                principal;


            // =========================================
            // ROUND VALUES
            // =========================================

            projectedValue =
                Math.Round(
                    projectedValue,
                    2
                );

            projectedReturn =
                Math.Round(
                    projectedReturn,
                    2
                );


            // =========================================
            // DISPLAY RESULT
            // =========================================

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
                $"Projected Return: £{projectedReturn:F2}"
            );

            Console.WriteLine(
                $"Projected Investment Value: £{projectedValue:F2}"
            );
        }
    }
}