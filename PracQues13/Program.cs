using System;

namespace PracQues13
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double length;
            double width;
            double height;

            // -------------------------
            // LENGTH VALIDATION
            // -------------------------
            while (true)
            {
                Console.Write("Enter Length: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out length))
                {
                    if (double.IsFinite(length) &&
                        length > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Length must be greater than zero."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric length."
                    );
                }
            }


            // -------------------------
            // WIDTH VALIDATION
            // -------------------------
            while (true)
            {
                Console.Write("Enter Width: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out width))
                {
                    if (double.IsFinite(width) &&
                        width > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Width must be greater than zero."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric width."
                    );
                }
            }


            // -------------------------
            // HEIGHT VALIDATION
            // -------------------------
            while (true)
            {
                Console.Write("Enter Height: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out height))
                {
                    if (double.IsFinite(height) &&
                        height > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Height must be greater than zero."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric height."
                    );
                }
            }


            // -------------------------
            // CALCULATE VOLUME
            // -------------------------

            double volume =
                length *
                width *
                height;


            // -------------------------
            // DISPLAY RESULT
            // -------------------------

            Console.WriteLine(
                "\n--- PACKAGE DETAILS ---"
            );

            Console.WriteLine(
                $"Length: {length:F2}"
            );

            Console.WriteLine(
                $"Width: {width:F2}"
            );

            Console.WriteLine(
                $"Height: {height:F2}"
            );

            Console.WriteLine(
                $"Package Volume: {volume:F2} cubic units"
            );
        }
    }
}