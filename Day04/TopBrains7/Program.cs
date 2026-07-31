using System;

public class Programming
{
    static int SumOfDigits(int n)
    {
        int sum = 0;
        while (n > 0)
        {
            sum += n % 10;
            n /= 10;
        }
        return sum;
    }

    static bool IsPrime(int n)
    {
        if (n < 2)
            return false;

        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0)
                return false;
        }

        return true;
    }

    static bool IsLuckyNumber(int x)
    {
        if (IsPrime(x))
            return false;

        int s = SumOfDigits(x);
        int squareSum = SumOfDigits(x * x);

        return squareSum == s * s;
    }

    public static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();

        int m = int.Parse(input[0]);
        int n = int.Parse(input[1]);

        int count = 0;

        for (int i = m; i <= n; i++)
        {
            if (IsLuckyNumber(i))
                count++;
        }

        Console.WriteLine(count);
    }
}