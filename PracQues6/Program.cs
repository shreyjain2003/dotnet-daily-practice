using System;
namespace PracQues6
{
    //interface
    public interface IBillingRule
    {
        double CalculateBill(double units, double rate, double fixedCharge);
    }

    //Residential Billing
    public class ResidentialBilling : IBillingRule
    {
        public double CalculateBill(double units, double rate, double fixedCharge)
        {
            return (units * rate) + fixedCharge;
        }
    }

    //Commercial Billing
    public class CommercialBilling : IBillingRule
    {
        public double CalculateBill(double units, double rate, double fixedCharge)
        {
            double baseBill = (units * rate) + fixedCharge;
            double surcharge = baseBill * 0.10;
            return baseBill + surcharge;
        }
    }

    public class BillingCalculator
    {
        private IBillingRule billingRule;
        public BillingCalculator(IBillingRule billingRule)
        {
            this.billingRule = billingRule;
        }

        public double CalculateBill(double units, double rate, double fixedCharge)
        {
            return billingRule.CalculateBill(units, rate, fixedCharge);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            string CustomerType;
            double units;
            double rate;
            double fixedCharge;

            while(true)
            {
                Console.WriteLine("Enter Customer type(1->Residential, 2->Commercial): ");
                Console.WriteLine("1 or 2 : ");
                string? input = Console.ReadLine();
                if(input == "1")
                {
                    CustomerType = "Residential";
                    break;
                }
                else if(input == "2")
                {
                    CustomerType = "Commercial";
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Please enter either 1 or 2.");
                }
            }

            while(true)
            {
                Console.WriteLine("Enter the number of units consumed: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input, out units))
                {
                    if(units >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Units cannot be negative");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter valid units.");
                }
            }
            while(true)
            {
                Console.WriteLine("Enter Rate per nuit: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input, out rate))
                {
                    if(rate >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Rates cannot be in negative");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter valid rates.");
                }
            }

            while(true)
            {
                Console.WriteLine("Enter fixed charge: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input, out fixedCharge))
                {
                    if(fixedCharge >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Fixed charge cannot be in negative.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter valid fixed charges.");
                }
            }
            IBillingRule billingrule;
            if(CustomerType == "Residential")
            {
                billingrule = new ResidentialBilling();
            }
            else
            {
                billingrule = new CommercialBilling();
            }

            BillingCalculator calculator = new BillingCalculator(billingrule);
            double finalBill = Math.Round(calculator.CalculateBill(units, rate, fixedCharge),2);

            Console.WriteLine("---ELECTRICITY BILL---");
            Console.WriteLine($"Customer Type : {CustomerType}");
            Console.WriteLine($"Units consumer are {units}");
            Console.WriteLine($"Rate per unit is {rate}");
            Console.WriteLine($"Final Bill is: {finalBill}");
        }
    }

}