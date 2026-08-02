using System;

namespace BankTransactionValidator
{
    class Account
    {
        public string AccountType;
        public double CurrentBalance;
        public double WithdrawalAmount;
        public double TransactionsToday;
        public int TransactionsThisMonth;

        public Account(string accountType, double currentBalance,
                       double withdrawalAmount, double transactionsToday,
                       int transactionsThisMonth)
        {
            AccountType = accountType;
            CurrentBalance = currentBalance;
            WithdrawalAmount = withdrawalAmount;
            TransactionsToday = transactionsToday;
            TransactionsThisMonth = transactionsThisMonth;
        }

        public void ValidateTransaction()
        {
            double fee = 0;

            if (WithdrawalAmount > 1000)
            {
                Console.WriteLine("Transaction Status : Denied");
                Console.WriteLine("Reason             : Maximum withdrawal per transaction is $1000");
                return;
            }

            if (TransactionsToday + WithdrawalAmount > 5000)
            {
                Console.WriteLine("Transaction Status : Denied");
                Console.WriteLine("Reason             : Daily withdrawal limit exceeded");
                return;
            }

            if (AccountType.ToUpper() == "S" && TransactionsThisMonth >= 3)
            {
                fee = 1;
            }

            if (CurrentBalance - WithdrawalAmount - fee < 50)
            {
                Console.WriteLine("Transaction Status : Denied");
                Console.WriteLine("Reason             : Minimum balance of $50 must be maintained");
                return;
            }

            double newBalance = CurrentBalance - WithdrawalAmount - fee;

            Console.WriteLine("Transaction Status : Approved");
            Console.WriteLine($"Transaction Fee   : ${fee:F2}");
            Console.WriteLine($"New Balance       : ${newBalance:F2}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Account Type (S/C): ");
            string accountType = Console.ReadLine();

            Console.Write("Enter Current Balance: ");
            double currentBalance = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Withdrawal Amount: ");
            double withdrawalAmount = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Today's Total Withdrawals: ");
            double transactionsToday = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Transactions This Month: ");
            int transactionsThisMonth = Convert.ToInt32(Console.ReadLine());

            Account account = new Account(accountType, currentBalance,
                                          withdrawalAmount,
                                          transactionsToday,
                                          transactionsThisMonth);

            Console.WriteLine("\n----- Transaction Result -----");
            account.ValidateTransaction();
        }
    }
}