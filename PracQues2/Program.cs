using System;
namespace PracQues2
{
    public class PracQues2
    {
        public class Userinfo
        {
            public double height;
            public double weight;

        }
        public static void Main(string[] args)
        {
            Userinfo user = new Userinfo();

            while(true)
            {
                Console.WriteLine("Enter height: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input, out user.height))
                {
                    if(user.height >0 )
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Height cannot be less than or equal to zero!");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter correct height.");
                }
            }

            while(true)
            {
                Console.WriteLine("Enter Weight: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input,out user.weight))
                {
                    if(user.weight > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Weight cannot be Less than or equal to Zero.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter valid Weight.");
                }
            }
            double bmi = user.weight / (user.height * user.height);

            bmi = Math.Round(bmi,2);
            string category;
            if(bmi < 18.5)
            {
                category = "Underweight";
            }
            else if(bmi < 25)
            {
                category = "Normal weight";
            }
            else if(bmi < 30)
            {
                category = "Overweight";
            }
            else
            {
                category = "Obese";
            }

            Console.WriteLine("BMI RESULT:");
            Console.WriteLine("Height: "+user.height+" m");
            Console.WriteLine("Weight: "+user.weight+" kg");
            Console.WriteLine("BMI: "+bmi);
            Console.WriteLine("Category: "+category);
        }
    }
}