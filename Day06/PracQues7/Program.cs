using System;

namespace MovieTicketBookingSystem
{
    class Ticket
    {
        public int Age;
        public bool IsStudent;
        public bool Is3D;
        public int NumberOfTickets;
        public int ShowHour;

        public Ticket(int age, bool isStudent, bool is3D, int numberOfTickets, int showHour)
        {
            Age = age;
            IsStudent = isStudent;
            Is3D = is3D;
            NumberOfTickets = numberOfTickets;
            ShowHour = showHour;
        }

        public double CalculatePricePerTicket()
        {
            double price = 12;

            // Age Discount
            if (Age < 12)
                price -= price * 0.30;
            else if (Age >= 60)
                price -= price * 0.25;
            else if (IsStudent)
                price -= price * 0.20;

            // Time-based Pricing
            if (ShowHour < 17)
                price -= 2;
            else
                price += 3;

            // 3D Charges
            if (Is3D)
                price += 5;

            // Group Discount
            if (NumberOfTickets >= 6)
                price -= price * 0.10;

            return price;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Show Time (24-hour format): ");
            int showHour = int.Parse(Console.ReadLine());

            Console.Write("Are you a Student? (Y/N): ");
            bool isStudent = Console.ReadLine().ToUpper() == "Y";

            Console.Write("Is it a 3D Movie? (Y/N): ");
            bool is3D = Console.ReadLine().ToUpper() == "Y";

            Console.Write("Enter Number of Tickets: ");
            int tickets = int.Parse(Console.ReadLine());

            Ticket ticket = new Ticket(age, isStudent, is3D, tickets, showHour);

            double pricePerTicket = ticket.CalculatePricePerTicket();
            double totalPrice = pricePerTicket * tickets;

            Console.WriteLine("\n------ Bill ------");
            Console.WriteLine($"Price Per Ticket : ${pricePerTicket:F2}");
            Console.WriteLine($"Total Price      : ${totalPrice:F2}");
        }
    }
}