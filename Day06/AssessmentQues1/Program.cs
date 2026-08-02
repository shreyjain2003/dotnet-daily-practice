using System;

namespace BankSys
{
    public class Account
    {
        // Private fields to store account holder's name and balance
        private string name;
        private double balance;

        // Constructor to initialize the account
        public Account(string name, double initialBalance)
        {
            this.name = name;
            this.balance = initialBalance;
        }

        // Adds the deposit amount to the current balance
        // and returns the updated balance
        public double deposit(double amount)
        {
            balance += amount;
            return balance;
        }

        // Returns the current account balance
        public double getBalance()
        {
            return balance;
        }

        // Updates the account holder's name
        public void setName(string newName)
        {
            name = newName;
        }

        // Returns the account holder's name
        public string getName()
        {
            return name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new account with initial details
            Account account1 = new Account("John Doe", 1250);

            // Display initial balance
            Console.WriteLine(account1.getBalance());

            // Display account holder's name
            Console.WriteLine(account1.getName());

            // Deposit an amount and display updated balance
            Console.WriteLine(account1.deposit(-750)); // Output: 500

            // Deposit another amount and display updated balance
            Console.WriteLine(account1.deposit(750.5)); // Output: 1250.5

            // Display current balance
            Console.WriteLine(account1.getBalance());

            // Update account holder's name
            account1.setName("Riya Amit Mehta");

            // Display updated account holder's name
            Console.WriteLine(account1.getName());
        }
    }
}