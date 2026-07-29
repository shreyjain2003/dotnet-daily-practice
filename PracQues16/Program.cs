using System;

namespace PracQues16
{
    // =========================================
    // INTERFACE
    // =========================================

    public interface IBillingRule
    {
        double CalculateBill(
            double units,
            double rate,
            double fixedCharge
        );
    }


    // =========================================
    // RESIDENTIAL BILLING
    // =========================================

    public class ResidentialBilling : IBillingRule
    {
        public double CalculateBill(
            double units,
            double rate,
            double fixedCharge)
        {
            return (units * rate) + fixedCharge;
        }
    }


    // =========================================
    // COMMERCIAL BILLING
    // =========================================

    public class CommercialBilling : IBillingRule
    {
        public double CalculateBill(
            double units,
            double rate,
            double fixedCharge)
        {
            double baseBill =
                (units * rate) + fixedCharge;

            double surcharge =
                baseBill * 0.10;

            return baseBill + surcharge;
        }
    }


    // =========================================
    // BILLING CALCULATOR
    // =========================================

    public class BillingCalculator
    {
        private readonly IBillingRule billingRule;

        public BillingCalculator(
            IBillingRule billingRule)
        {
            this.billingRule = billingRule;
        }

        public double CalculateBill(
            double units,
            double rate,
            double fixedCharge)
        {
            return billingRule.CalculateBill(
                units,
                rate,
                fixedCharge
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
            string customerType;
            double units;
            double rate;
            double fixedCharge;


            // =========================================
            // CUSTOMER TYPE VALIDATION
            // =========================================

            while (true)
            {
                Console.WriteLine(
                    "\nSelect Customer Type:"
                );

                Console.WriteLine(
                    "1. Residential"
                );

                Console.WriteLine(
                    "2. Commercial"
                );

                Console.Write(
                    "Enter your choice: "
                );

                string? input =
                    Console.ReadLine();

                if (input == "1")
                {
                    customerType =
                        "Residential";

                    break;
                }
                else if (input == "2")
                {
                    customerType =
                        "Commercial";

                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter 1 or 2."
                    );
                }
            }


            // =========================================
            // UNITS VALIDATION
            // =========================================

            while (true)
            {
                Console.Write(
                    "\nEnter units consumed: "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out units))
                {
                    if (double.IsFinite(units) &&
                        units >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Units cannot be negative."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric value."
                    );
                }
            }


            // =========================================
            // RATE VALIDATION
            // =========================================

            while (true)
            {
                Console.Write(
                    "\nEnter rate per unit: "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out rate))
                {
                    if (double.IsFinite(rate) &&
                        rate >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Rate cannot be negative."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric rate."
                    );
                }
            }


            // =========================================
            // FIXED CHARGE VALIDATION
            // =========================================

            while (true)
            {
                Console.Write(
                    "\nEnter fixed charge: "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out fixedCharge))
                {
                    if (double.IsFinite(fixedCharge) &&
                        fixedCharge >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Fixed charge cannot be negative."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric fixed charge."
                    );
                }
            }


            // =========================================
            // SELECT BILLING STRATEGY
            // =========================================

            IBillingRule billingRule;

            if (customerType == "Residential")
            {
                billingRule =
                    new ResidentialBilling();
            }
            else
            {
                billingRule =
                    new CommercialBilling();
            }


            // =========================================
            // CALCULATE BILL
            // =========================================

            BillingCalculator calculator =
                new BillingCalculator(
                    billingRule
                );

            double finalBill =
                calculator.CalculateBill(
                    units,
                    rate,
                    fixedCharge
                );


            // =========================================
            // FINAL VALIDATION
            // =========================================

            if (!double.IsFinite(finalBill) ||
                finalBill < 0)
            {
                Console.WriteLine(
                    "Error: Unable to calculate a valid bill."
                );

                return;
            }


            // =========================================
            // ROUND BILL
            // =========================================

            finalBill =
                Math.Round(finalBill, 2);


            // =========================================
            // DISPLAY BILL
            // =========================================

            Console.WriteLine(
                "\n--- ELECTRICITY BILL ---"
            );

            Console.WriteLine(
                $"Customer Type: {customerType}"
            );

            Console.WriteLine(
                $"Units Consumed: {units:F2}"
            );

            Console.WriteLine(
                $"Rate per Unit: £{rate:F2}"
            );

            Console.WriteLine(
                $"Fixed Charge: £{fixedCharge:F2}"
            );

            Console.WriteLine(
                $"Final Bill: £{finalBill:F2}"
            );
        }
    }
}