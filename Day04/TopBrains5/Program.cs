using System;
public class LargestInteger
{
    public int FindLargest(int a, int b, int c)
    {
        if(a > b && a > c)
        {
            return a;
        }
        else if(b > a && b > c)
        {
            return b;
        }
        else
        {
            return c;
        }
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter num1: ");
        int num1 = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter num2: ");
        int num2 = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter num3: ");
        int num3 = int.Parse(Console.ReadLine());

        LargestInteger largestint = new LargestInteger();
        int result = largestint.FindLargest(num1, num2, num3);
        Console.WriteLine(result);


    }
}