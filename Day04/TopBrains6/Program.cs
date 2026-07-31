using System;

public class DisplayHeight
{
    public void Rules(double height)
    {
        if(height < 150)
        {
            Console.WriteLine("Your height is: Short!");
        }
        else if(height >= 150 && height < 180)
        {
            Console.WriteLine("Your height is: Average!");
        }
        else
        {
            Console.WriteLine("Your height is: Tall!");
        }
        
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter your height in cm: ");
        double height = double.Parse(Console.ReadLine());
        DisplayHeight display = new DisplayHeight();
        display.Rules(height);
    }
}