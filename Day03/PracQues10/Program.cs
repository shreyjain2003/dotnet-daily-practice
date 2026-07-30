using System;
using System.Collections.Generic;
using System.Linq;

namespace PracQues10
{
    // Represents a customer who either travels on a cruise
    // or sends cargo using a cargo ship.
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }

        public Customer(
            int customerId,
            string customerName,
            string contactNumber)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            ContactNumber = contactNumber;
        }

        // Displays customer details.
        public void Display()
        {
            Console.WriteLine(
                $"Customer ID: {CustomerId}");

            Console.WriteLine(
                $"Customer Name: {CustomerName}");

            Console.WriteLine(
                $"Contact Number: {ContactNumber}");

            Console.WriteLine(
                "----------------------------------------");
        }
    }


    // Base class representing a ship.
    public abstract class Ship
    {
        public int ShipId { get; set; }
        public string ShipName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

        protected Ship(
            int shipId,
            string shipName,
            string source,
            string destination)
        {
            ShipId = shipId;
            ShipName = shipName;
            Source = source;
            Destination = destination;
        }

        // Displays common ship information.
        public virtual void Display()
        {
            Console.WriteLine(
                $"Ship ID: {ShipId}");

            Console.WriteLine(
                $"Ship Name: {ShipName}");

            Console.WriteLine(
                $"Route: {Source} -> {Destination}");
        }
    }


    // Represents a cruise ship used for passenger travel.
    public class CruiseShip : Ship
    {
        public CruiseShip(
            int shipId,
            string shipName,
            string source,
            string destination)
            : base(
                shipId,
                shipName,
                source,
                destination)
        {
        }

        // Displays cruise ship details.
        public override void Display()
        {
            Console.WriteLine(
                "Ship Type: Cruise");

            base.Display();

            Console.WriteLine(
                "----------------------------------------");
        }
    }


    // Represents a cargo ship used for transporting cargo.
    public class CargoShip : Ship
    {
        public CargoShip(
            int shipId,
            string shipName,
            string source,
            string destination)
            : base(
                shipId,
                shipName,
                source,
                destination)
        {
        }

        // Displays cargo ship details.
        public override void Display()
        {
            Console.WriteLine(
                "Ship Type: Cargo");

            base.Display();

            Console.WriteLine(
                "----------------------------------------");
        }
    }


    // Represents a booking made by a customer.
    public class Booking
    {
        public int BookingId { get; set; }
        public Customer Customer { get; set; }
        public Ship Ship { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking(
            int bookingId,
            Customer customer,
            Ship ship,
            decimal amountPaid)
        {
            BookingId = bookingId;
            Customer = customer;
            Ship = ship;
            AmountPaid = amountPaid;
            BookingDate = DateTime.Now;
        }

        // Displays booking details.
        public void Display()
        {
            Console.WriteLine(
                $"Booking ID: {BookingId}");

            Console.WriteLine(
                $"Customer: {Customer.CustomerName}");

            Console.WriteLine(
                $"Ship: {Ship.ShipName}");

            Console.WriteLine(
                $"Amount Paid: ₹{AmountPaid:F2}");

            Console.WriteLine(
                $"Booking Date: {BookingDate:dd-MM-yyyy}");

            Console.WriteLine(
                "----------------------------------------");
        }
    }


    // Manages ships, customers, and bookings.
    public class MarineCompany
    {
        private readonly List<Ship> ships;
        private readonly List<Customer> customers;
        private readonly List<Booking> bookings;

        public MarineCompany()
        {
            ships =
                new List<Ship>();

            customers =
                new List<Customer>();

            bookings =
                new List<Booking>();
        }


        // Adds a ship to the marine company.
        public bool AddShip(
            Ship ship)
        {
            // Prevent duplicate ship IDs.
            if (ships.Any(
                existingShip =>
                    existingShip.ShipId ==
                    ship.ShipId))
            {
                Console.WriteLine(
                    "Error: Ship ID already exists.");

                return false;
            }

            ships.Add(
                ship);

            return true;
        }


        // Adds a customer to the marine company.
        public bool AddCustomer(
            Customer customer)
        {
            // Prevent duplicate customer IDs.
            if (customers.Any(
                existingCustomer =>
                    existingCustomer.CustomerId ==
                    customer.CustomerId))
            {
                Console.WriteLine(
                    "Error: Customer ID already exists.");

                return false;
            }

            customers.Add(
                customer);

            return true;
        }


        // Finds a ship using its ID.
        private Ship? GetShipById(
            int shipId)
        {
            return ships.FirstOrDefault(
                ship =>
                    ship.ShipId ==
                    shipId);
        }


        // Finds a customer using their ID.
        private Customer? GetCustomerById(
            int customerId)
        {
            return customers.FirstOrDefault(
                customer =>
                    customer.CustomerId ==
                    customerId);
        }


        // Registers a customer for a particular ship.
        public bool RegisterCustomer(
            int bookingId,
            int customerId,
            int shipId,
            decimal amountPaid)
        {
            // Find customer.
            Customer? customer =
                GetCustomerById(
                    customerId);

            if (customer == null)
            {
                Console.WriteLine(
                    "Error: Customer not found.");

                return false;
            }


            // Find ship.
            Ship? ship =
                GetShipById(
                    shipId);

            if (ship == null)
            {
                Console.WriteLine(
                    "Error: Ship not found.");

                return false;
            }


            // Validate payment amount.
            if (amountPaid <= 0)
            {
                Console.WriteLine(
                    "Error: Amount must be greater than zero.");

                return false;
            }


            // Prevent duplicate booking IDs.
            if (bookings.Any(
                booking =>
                    booking.BookingId ==
                    bookingId))
            {
                Console.WriteLine(
                    "Error: Booking ID already exists.");

                return false;
            }


            // Create booking.
            Booking newBooking =
                new Booking(
                    bookingId,
                    customer,
                    ship,
                    amountPaid);


            // Store booking.
            bookings.Add(
                newBooking);

            Console.WriteLine(
                $"Customer '{customer.CustomerName}' " +
                $"registered successfully on '{ship.ShipName}'.");

            return true;
        }


        // Calculates the total amount collected by the marine company.
        public decimal GetTotalAmountCollected()
        {
            return bookings.Sum(
                booking =>
                    booking.AmountPaid);
        }


        // Calculates the total amount collected for a particular ship.
        public decimal GetAmountCollectedForShip(
            int shipId)
        {
            return bookings
                .Where(
                    booking =>
                        booking.Ship.ShipId ==
                        shipId)
                .Sum(
                    booking =>
                        booking.AmountPaid);
        }


        // Displays the total amount collected for every ship.
        public void DisplayAmountForEveryShip()
        {
            Console.WriteLine(
                "========== TOTAL AMOUNT FOR EVERY SHIP ==========");

            foreach (Ship ship in ships)
            {
                decimal totalAmount =
                    GetAmountCollectedForShip(
                        ship.ShipId);

                Console.WriteLine(
                    $"{ship.ShipName}: ₹{totalAmount:F2}");
            }
        }


        // Displays total amount collected by the marine company.
        public void DisplayTotalAmountCollected()
        {
            decimal totalAmount =
                GetTotalAmountCollected();

            Console.WriteLine(
                "========== TOTAL AMOUNT COLLECTED ==========");

            Console.WriteLine(
                $"Total Amount Collected: ₹{totalAmount:F2}");
        }


        // Displays all customers registered on a particular cruise ship.
        public void DisplayCustomersForCruiseShip(
            int shipId)
        {
            // Find ship.
            Ship? ship =
                GetShipById(
                    shipId);

            if (ship == null)
            {
                Console.WriteLine(
                    "Error: Ship not found.");

                return;
            }


            // Verify that the selected ship is a cruise ship.
            if (ship is not CruiseShip)
            {
                Console.WriteLine(
                    "Error: The selected ship is not a cruise ship.");

                return;
            }


            Console.WriteLine(
                $"========== CUSTOMERS ON {ship.ShipName.ToUpper()} ==========");


            // Find all bookings for the selected cruise ship.
            List<Customer> cruiseCustomers =
                bookings
                    .Where(
                        booking =>
                            booking.Ship.ShipId ==
                            shipId)
                    .Select(
                        booking =>
                            booking.Customer)
                    .DistinctBy(
                        customer =>
                            customer.CustomerId)
                    .ToList();


            if (cruiseCustomers.Count == 0)
            {
                Console.WriteLine(
                    "No customers registered on this cruise ship.");

                return;
            }


            // Display customer details.
            foreach (
                Customer customer
                in cruiseCustomers)
            {
                customer.Display();
            }
        }


        // Displays all ships in the marine company.
        public void DisplayAllShips()
        {
            Console.WriteLine(
                "========== ALL SHIPS ==========");

            foreach (Ship ship in ships)
            {
                ship.Display();
            }
        }


        // Displays all bookings.
        public void DisplayAllBookings()
        {
            Console.WriteLine(
                "========== ALL BOOKINGS ==========");

            if (bookings.Count == 0)
            {
                Console.WriteLine(
                    "No bookings found.");

                return;
            }

            foreach (
                Booking booking
                in bookings)
            {
                booking.Display();
            }
        }
    }


    // Application entry point.
    public class Program
    {
        public static void Main(string[] args)
        {
            // ==========================================
            // CREATE MARINE COMPANY
            // ==========================================

            MarineCompany company =
                new MarineCompany();


            // ==========================================
            // ADD CRUISE SHIPS
            // ==========================================

            company.AddShip(
                new CruiseShip(
                    101,
                    "Ocean Pearl",
                    "Mumbai",
                    "Goa"));

            company.AddShip(
                new CruiseShip(
                    102,
                    "Sea Explorer",
                    "Chennai",
                    "Andaman"));


            // ==========================================
            // ADD CARGO SHIPS
            // ==========================================

            company.AddShip(
                new CargoShip(
                    201,
                    "Cargo Express",
                    "Mumbai",
                    "Dubai"));

            company.AddShip(
                new CargoShip(
                    202,
                    "Ocean Carrier",
                    "Kolkata",
                    "Singapore"));


            // ==========================================
            // ADD CUSTOMERS
            // ==========================================

            company.AddCustomer(
                new Customer(
                    1,
                    "Shrey Jain",
                    "9876543210"));

            company.AddCustomer(
                new Customer(
                    2,
                    "Rahul Sharma",
                    "9876501234"));

            company.AddCustomer(
                new Customer(
                    3,
                    "Priya Verma",
                    "9876512345"));

            company.AddCustomer(
                new Customer(
                    4,
                    "Amit Kumar",
                    "9876523456"));


            // ==========================================
            // DISPLAY ALL SHIPS
            // ==========================================

            company.DisplayAllShips();


            // ==========================================
            // REGISTER CUSTOMERS FOR SHIPS
            // ==========================================

            Console.WriteLine(
                "\n========== CUSTOMER REGISTRATIONS ==========");

            // Customers traveling on cruise ships.
            company.RegisterCustomer(
                1001,
                1,
                101,
                15000.00m);

            company.RegisterCustomer(
                1002,
                2,
                101,
                15000.00m);

            company.RegisterCustomer(
                1003,
                3,
                102,
                18000.00m);


            // Customers sending cargo using cargo ships.
            company.RegisterCustomer(
                1004,
                4,
                201,
                25000.00m);

            company.RegisterCustomer(
                1005,
                1,
                202,
                30000.00m);


            // ==========================================
            // DISPLAY ALL BOOKINGS
            // ==========================================

            Console.WriteLine();

            company.DisplayAllBookings();


            // ==========================================
            // DISPLAY TOTAL AMOUNT COLLECTED
            // ==========================================

            Console.WriteLine();

            company.DisplayTotalAmountCollected();


            // ==========================================
            // DISPLAY TOTAL AMOUNT FOR EVERY SHIP
            // ==========================================

            Console.WriteLine();

            company.DisplayAmountForEveryShip();


            // ==========================================
            // DISPLAY CUSTOMERS FOR PARTICULAR CRUISE
            // ==========================================

            Console.WriteLine();

            company.DisplayCustomersForCruiseShip(
                101);
        }
    }
}