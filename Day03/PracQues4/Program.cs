using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketBookingSystem
{
    // Represents a movie or event available for booking.
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Venue { get; set; }
        public DateTime EventDate { get; set; }
        public decimal TicketPrice { get; set; }
        public int TotalSeats { get; set; }

        public Event(
            int eventId,
            string eventName,
            string venue,
            DateTime eventDate,
            decimal ticketPrice,
            int totalSeats)
        {
            EventId = eventId;
            EventName = eventName;
            Venue = venue;
            EventDate = eventDate;
            TicketPrice = ticketPrice;
            TotalSeats = totalSeats;
        }

        // Displays event details.
        public void Display()
        {
            Console.WriteLine($"Event ID      : {EventId}");
            Console.WriteLine($"Event Name    : {EventName}");
            Console.WriteLine($"Venue         : {Venue}");
            Console.WriteLine($"Event Date    : {EventDate:dd-MM-yyyy HH:mm}");
            Console.WriteLine($"Ticket Price  : ₹{TicketPrice:F2}");
            Console.WriteLine($"Available Seats: {TotalSeats}");
            Console.WriteLine("----------------------------------------");
        }
    }


    // Represents a customer who books tickets.
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }

        public Customer(
            int customerId,
            string customerName,
            string email)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Email = email;
        }
    }


    // Represents a ticket booking made by a customer.
    public class Booking
    {
        public int BookingId { get; set; }
        public Customer Customer { get; set; }
        public Event Event { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalAmount { get; set; }

        public Booking(
            int bookingId,
            Customer customer,
            Event eventDetails,
            int numberOfTickets)
        {
            BookingId = bookingId;
            Customer = customer;
            Event = eventDetails;
            NumberOfTickets = numberOfTickets;
            TotalAmount =
                eventDetails.TicketPrice * numberOfTickets;
        }

        // Displays booking details.
        public void Display()
        {
            Console.WriteLine($"Booking ID      : {BookingId}");
            Console.WriteLine(
                $"Customer Name   : {Customer.CustomerName}");
            Console.WriteLine(
                $"Event Name      : {Event.EventName}");
            Console.WriteLine(
                $"Number of Tickets: {NumberOfTickets}");
            Console.WriteLine(
                $"Total Amount    : ₹{TotalAmount:F2}");
            Console.WriteLine("----------------------------------------");
        }
    }


    // Manages events, customers, and ticket bookings.
    public class BookingSystem
    {
        private readonly List<Event> events;
        private readonly List<Customer> customers;
        private readonly List<Booking> bookings;

        public BookingSystem()
        {
            events = new List<Event>();
            customers = new List<Customer>();
            bookings = new List<Booking>();
        }


        // Adds an event to the booking system.
        public void AddEvent(Event eventDetails)
        {
            events.Add(eventDetails);
        }


        // Adds a customer to the booking system.
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }


        // Returns all available events.
        public List<Event> GetAllEvents()
        {
            return events;
        }


        // Searches for an event by its unique ID.
        public Event? GetEventById(int eventId)
        {
            return events.FirstOrDefault(
                eventDetails =>
                    eventDetails.EventId == eventId);
        }


        // Creates a booking after validating seat availability.
        public bool BookTickets(
            int bookingId,
            int customerId,
            int eventId,
            int numberOfTickets)
        {
            // Validate ticket quantity.
            if (numberOfTickets <= 0)
            {
                Console.WriteLine(
                    "Error: Number of tickets must be greater than zero.");

                return false;
            }


            // Find the customer.
            Customer? customer =
                customers.FirstOrDefault(
                    c => c.CustomerId == customerId);

            if (customer == null)
            {
                Console.WriteLine(
                    "Error: Customer not found.");

                return false;
            }


            // Find the requested event.
            Event? eventDetails =
                GetEventById(eventId);

            if (eventDetails == null)
            {
                Console.WriteLine(
                    "Error: Event not found.");

                return false;
            }


            // Check available seats.
            if (numberOfTickets >
                eventDetails.TotalSeats)
            {
                Console.WriteLine(
                    "Error: Not enough seats available.");

                return false;
            }


            // Create the booking.
            Booking booking =
                new Booking(
                    bookingId,
                    customer,
                    eventDetails,
                    numberOfTickets);


            // Reduce available seats.
            eventDetails.TotalSeats -=
                numberOfTickets;


            // Store the booking.
            bookings.Add(booking);

            Console.WriteLine(
                "Booking completed successfully.");

            return true;
        }


        // Cancels an existing booking.
        public bool CancelBooking(int bookingId)
        {
            Booking? booking =
                bookings.FirstOrDefault(
                    b => b.BookingId == bookingId);

            if (booking == null)
            {
                Console.WriteLine(
                    "Error: Booking not found.");

                return false;
            }


            // Restore the seats to the event.
            booking.Event.TotalSeats +=
                booking.NumberOfTickets;


            // Remove the booking.
            bookings.Remove(booking);

            Console.WriteLine(
                "Booking cancelled successfully.");

            return true;
        }


        // Returns all bookings made by a specific customer.
        public List<Booking> GetBookingsByCustomer(
            int customerId)
        {
            return bookings
                .Where(
                    booking =>
                        booking.Customer.CustomerId ==
                        customerId)
                .ToList();
        }


        // Displays all events.
        public void DisplayAllEvents()
        {
            Console.WriteLine(
                "========== AVAILABLE EVENTS ==========");

            foreach (Event eventDetails in events)
            {
                eventDetails.Display();
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
                    "No bookings available.");

                return;
            }

            foreach (Booking booking in bookings)
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
            // Create the booking system.
            BookingSystem bookingSystem =
                new BookingSystem();


            // ==========================================
            // ADD EVENTS
            // ==========================================

            bookingSystem.AddEvent(
                new Event(
                    101,
                    "Avengers: Secret Wars",
                    "PVR Cinemas",
                    new DateTime(
                        2026,
                        8,
                        15,
                        18,
                        30,
                        0),
                    250.00m,
                    100));


            bookingSystem.AddEvent(
                new Event(
                    102,
                    "Live Music Concert",
                    "Indira Gandhi Stadium",
                    new DateTime(
                        2026,
                        9,
                        10,
                        19,
                        00,
                        0),
                    1000.00m,
                    500));


            // ==========================================
            // ADD CUSTOMERS
            // ==========================================

            bookingSystem.AddCustomer(
                new Customer(
                    1,
                    "Shrey Jain",
                    "shrey@example.com"));


            bookingSystem.AddCustomer(
                new Customer(
                    2,
                    "Rahul Sharma",
                    "rahul@example.com"));


            // ==========================================
            // DISPLAY EVENTS
            // ==========================================

            bookingSystem.DisplayAllEvents();


            // ==========================================
            // BOOK TICKETS
            // ==========================================

            Console.WriteLine(
                "\n========== BOOKING TICKETS ==========");

            bookingSystem.BookTickets(
                1001,
                1,
                101,
                3);


            bookingSystem.BookTickets(
                1002,
                2,
                102,
                2);


            // ==========================================
            // DISPLAY BOOKINGS
            // ==========================================

            Console.WriteLine();

            bookingSystem.DisplayAllBookings();


            // ==========================================
            // DISPLAY UPDATED EVENT AVAILABILITY
            // ==========================================

            Console.WriteLine(
                "\n========== UPDATED EVENT DETAILS ==========");

            bookingSystem.DisplayAllEvents();


            // ==========================================
            // FIND CUSTOMER BOOKINGS
            // ==========================================

            Console.WriteLine(
                "\n========== CUSTOMER BOOKINGS ==========");

            List<Booking> customerBookings =
                bookingSystem.GetBookingsByCustomer(1);

            foreach (Booking booking in customerBookings)
            {
                booking.Display();
            }


            // ==========================================
            // CANCEL BOOKING
            // ==========================================

            Console.WriteLine(
                "\n========== CANCEL BOOKING ==========");

            bookingSystem.CancelBooking(1001);


            // ==========================================
            // DISPLAY FINAL DETAILS
            // ==========================================

            Console.WriteLine(
                "\n========== FINAL BOOKING DETAILS ==========");

            bookingSystem.DisplayAllBookings();

            Console.WriteLine(
                "\n========== FINAL EVENT AVAILABILITY ==========");

            bookingSystem.DisplayAllEvents();
        }
    }
}