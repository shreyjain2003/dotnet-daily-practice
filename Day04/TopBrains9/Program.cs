using System;

public class BankTransactionProgram
{
    public int FinalBalance(int initialBalance, int[] transactions)
    {
        int balance = initialBalance;

        foreach (int transaction in transactions)
        {
            if (transaction >= 0)
            {
                balance += transaction;
            }
            else
            {
                if (balance >= -transaction)
                {
                    balance += transaction;
                }
            }
        }

        return balance;
    }

    public static void Main(string[] args)
    {
        int initialBalance = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());

        int[] transactions = new int[n];

        for (int i = 0; i < n; i++)
        {
            transactions[i] = int.Parse(Console.ReadLine());
        }

        BankTransactionProgram solution = new BankTransactionProgram();
        Console.WriteLine(solution.FinalBalance(initialBalance, transactions));
    }
}