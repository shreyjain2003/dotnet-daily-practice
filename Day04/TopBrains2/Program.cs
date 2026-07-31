using System;
namespace MultiplicationTable
{
    public class Program
    {
        public void Table(int num, int upto)
        {
            for(int i = 1;i <= upto;i++)
            {
                Console.Write(num * i + " ");
            }
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the number: ");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Upto limit: ");
            int upto = int.Parse(Console.ReadLine());

            Program p = new Program();
            p.Table(num,upto);
        }
    }
}