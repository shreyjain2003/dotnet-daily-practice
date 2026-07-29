using System;
namespace PracQues3
{
    public class Program
    {
        public class Measurements
        {
            public double length;
            public double width;
            public double height;
            public Measurements(double Length, double Width, double Height)
            {
                length = Length;
                width = Width;
                height = Height;
            }
            public double CalculateVolume()
            {
                return length * width * height;
            }
        }
        public static void Main(string[] args)
        {
            Program p = new Program();
            double length;
            double width;
            double height;
            while(true)
            {
                Console.WriteLine("Enter Length: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input, out length))
                {
                    if(length > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: length cannot be Negative or Zero.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid Length.");
                }
            }

            while(true)
            {
                Console.WriteLine("Enter Width: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input,out width))
                {
                    if(width > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Width cannot be zero or negative.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid width.");
                }
            }

            while(true)
            {
                Console.WriteLine("Enter Height: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input, out height))
                {
                    if(height > 0 )
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Width cannot be negative or zero.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid height.");
                }
            }
            Measurements measurements = new Measurements(length,width,height);

            double volume = measurements.CalculateVolume();
            Console.WriteLine($"Volume: {volume}");
        }
    }
}