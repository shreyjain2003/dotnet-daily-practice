using System;

namespace Swapping
{
    public class Program
    {
        // Method 1: Swap using ref parameters.
        public static void SwapUsingRef(
            ref int first,
            ref int second)
        {
            // Swap values using arithmetic operations.
            first = first + second;
            second = first - second;
            first = first - second;
        }


        // Method 2: Swap using out parameters.
        public static void SwapUsingOut(
            int first,
            int second,
            out int swappedFirst,
            out int swappedSecond)
        {
            // Assign swapped values to out parameters.
            swappedFirst = second;
            swappedSecond = first;
        }


        public static void Main(string[] args)
        {
            // ==========================================
            // METHOD 1: SWAP USING REF
            // ==========================================

            Console.WriteLine("Enter Num1: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Num2: ");
            int num2 = int.Parse(Console.ReadLine());

            Console.WriteLine(
                "Before Swap (ref):");

            Console.WriteLine(
                $"num1 = {num1}, num2 = {num2}");

            SwapUsingRef(
                ref num1,
                ref num2);

            Console.WriteLine(
                "After Swap (ref):");

            Console.WriteLine(
                $"num1 = {num1}, num2 = {num2}");


            // ==========================================
            // METHOD 2: SWAP USING OUT
            // ==========================================


            Console.WriteLine(
                "\nBefore Swap (out):");

            Console.WriteLine(
                $"num1 = {num1}, num2 = {num2}");

            int swappednum1;
            int swappednum2;

            SwapUsingOut(
                num1,
                num2,
                out swappednum1,
                out swappednum2);

            Console.WriteLine(
                "After Swap (out):");

            Console.WriteLine(
                $"num1 = {swappednum1}, num2 = {swappednum2}");
        }
    }
}