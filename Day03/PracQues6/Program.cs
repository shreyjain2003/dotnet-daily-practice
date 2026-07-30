using System;
using System.Collections.Generic;
using System.Linq;

namespace PracQues6
{
    // Represents a hotel room.
    public class Room
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        public Room(
            int roomNumber,
            string roomType,
            decimal pricePerNight)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            PricePerNight = pricePerNight;
            IsAvailable = true;
        }

        // Displays room details.
        public void Display()
        {
            Console.WriteLine($"Room Number    : {RoomNumber}");
            Console.WriteLine($"Room Type      : {RoomType}");
            Console.WriteLine($"Price Per Night: ₹{PricePerNight:F2}");
            Console.WriteLine(
                $"Availability   : {(IsAvailable ? "Available" : "Occupied")}");
            Console.WriteLine("----------------------------------------");
        }
    }


    // Represents a hotel guest.
    public class Guest
    {
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        public string PhoneNumber { get; set; }

        public Guest(
            int guestId,
            string guestName,
            string phoneNumber)
        {
            GuestId = guestId;
            GuestName = guestName;
            PhoneNumber = phoneNumber;
        }
    }


    // Represents a room reservation.
    public class Reservation
    {
        public int ReservationId { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Reservation(
            int reservationId,
            Guest guest,
            Room room,
            DateTime checkInDate,
            DateTime checkOutDate)
        {
            ReservationId = reservationId;
            Guest = guest;
            Room = room;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;

            int numberOfNights =
                (CheckOutDate - CheckInDate).Days;

            TotalAmount =
                numberOfNights * Room.PricePerNight;
        }

        // Displays reservation details.
        public void Display()
        {
            Console.WriteLine(
                $"Reservation ID : {ReservationId}");

            Console.WriteLine(
                $"Guest Name     : {Guest.GuestName}");

            Console.WriteLine(
                $"Room Number    : {Room.RoomNumber}");

            Console.WriteLine(
                $"Room Type      : {Room.RoomType}");

            Console.WriteLine(
                $"Check-in Date  : {CheckInDate:dd-MM-yyyy}");

            Console.WriteLine(
                $"Check-out Date : {CheckOutDate:dd-MM-yyyy}");

            Console.WriteLine(
                $"Total Amount   : ₹{TotalAmount:F2}");

            Console.WriteLine("----------------------------------------");
        }
    }


    // Manages hotel rooms, guests, and reservations.
    public class Hotel
    {
        private readonly List<Room> rooms;
        private readonly List<Guest> guests;
        private readonly List<Reservation> reservations;

        public Hotel()
        {
            rooms = new List<Room>();
            guests = new List<Guest>();
            reservations = new List<Reservation>();
        }


        // Adds a room to the hotel.
        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }


        // Registers a new guest.
        public void AddGuest(Guest guest)
        {
            guests.Add(guest);
        }


        // Returns all rooms.
        public List<Room> GetAllRooms()
        {
            return rooms;
        }


        // Returns only currently available rooms.
        public List<Room> GetAvailableRooms()
        {
            return rooms
                .Where(room => room.IsAvailable)
                .ToList();
        }


        // Returns rooms filtered by room type.
        public List<Room> GetRoomsByType(
            string roomType)
        {
            return rooms
                .Where(
                    room =>
                        room.RoomType.Equals(
                            roomType,
                            StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        // Creates a reservation after validating
        // guest, room, and date availability.
        public bool MakeReservation(
            int reservationId,
            int guestId,
            int roomNumber,
            DateTime checkInDate,
            DateTime checkOutDate)
        {
            // Validate reservation dates.
            if (checkOutDate <= checkInDate)
            {
                Console.WriteLine(
                    "Error: Check-out date must be after check-in date.");

                return false;
            }


            // Find the guest.
            Guest? guest =
                guests.FirstOrDefault(
                    g => g.GuestId == guestId);

            if (guest == null)
            {
                Console.WriteLine(
                    "Error: Guest not found.");

                return false;
            }


            // Find the requested room.
            Room? room =
                rooms.FirstOrDefault(
                    r => r.RoomNumber == roomNumber);

            if (room == null)
            {
                Console.WriteLine(
                    "Error: Room not found.");

                return false;
            }


            // Check whether the room is currently available.
            if (!room.IsAvailable)
            {
                Console.WriteLine(
                    "Error: Room is already occupied.");

                return false;
            }


            // Prevent duplicate reservation IDs.
            if (reservations.Any(
                reservation =>
                    reservation.ReservationId ==
                    reservationId))
            {
                Console.WriteLine(
                    "Error: Reservation ID already exists.");

                return false;
            }


            // Create the reservation.
            Reservation reservation =
                new Reservation(
                    reservationId,
                    guest,
                    room,
                    checkInDate,
                    checkOutDate);


            // Mark the room as occupied.
            room.IsAvailable = false;


            // Store the reservation.
            reservations.Add(reservation);

            Console.WriteLine(
                "Reservation created successfully.");

            return true;
        }


        // Cancels an existing reservation.
        public bool CancelReservation(
            int reservationId)
        {
            Reservation? reservation =
                reservations.FirstOrDefault(
                    r =>
                        r.ReservationId ==
                        reservationId);

            if (reservation == null)
            {
                Console.WriteLine(
                    "Error: Reservation not found.");

                return false;
            }


            // Make the room available again.
            reservation.Room.IsAvailable = true;


            // Remove the reservation.
            reservations.Remove(reservation);

            Console.WriteLine(
                "Reservation cancelled successfully.");

            return true;
        }


        // Returns reservations made by a specific guest.
        public List<Reservation> GetReservationsByGuest(
            int guestId)
        {
            return reservations
                .Where(
                    reservation =>
                        reservation.Guest.GuestId ==
                        guestId)
                .ToList();
        }


        // Displays all available rooms.
        public void DisplayAvailableRooms()
        {
            Console.WriteLine(
                "========== AVAILABLE ROOMS ==========");

            List<Room> availableRooms =
                GetAvailableRooms();

            if (availableRooms.Count == 0)
            {
                Console.WriteLine(
                    "No rooms are currently available.");

                return;
            }

            foreach (Room room in availableRooms)
            {
                room.Display();
            }
        }


        // Displays all reservations.
        public void DisplayAllReservations()
        {
            Console.WriteLine(
                "========== HOTEL RESERVATIONS ==========");

            if (reservations.Count == 0)
            {
                Console.WriteLine(
                    "No reservations found.");

                return;
            }

            foreach (Reservation reservation
                in reservations)
            {
                reservation.Display();
            }
        }
    }


    // Application entry point.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create hotel management system.
            Hotel hotel =
                new Hotel();


            // ==========================================
            // ADD HOTEL ROOMS
            // ==========================================

            hotel.AddRoom(
                new Room(
                    101,
                    "Single",
                    2000.00m));

            hotel.AddRoom(
                new Room(
                    102,
                    "Double",
                    3500.00m));

            hotel.AddRoom(
                new Room(
                    201,
                    "Suite",
                    6000.00m));

            hotel.AddRoom(
                new Room(
                    202,
                    "Deluxe",
                    4500.00m));


            // ==========================================
            // REGISTER GUESTS
            // ==========================================

            hotel.AddGuest(
                new Guest(
                    1,
                    "Shrey Jain",
                    "9876543210"));

            hotel.AddGuest(
                new Guest(
                    2,
                    "Rahul Sharma",
                    "9876501234"));


            // ==========================================
            // DISPLAY AVAILABLE ROOMS
            // ==========================================

            hotel.DisplayAvailableRooms();


            // ==========================================
            // CREATE RESERVATIONS
            // ==========================================

            Console.WriteLine(
                "\n========== MAKING RESERVATIONS ==========");

            hotel.MakeReservation(
                1001,
                1,
                101,
                new DateTime(
                    2026,
                    8,
                    1),
                new DateTime(
                    2026,
                    8,
                    4));


            hotel.MakeReservation(
                1002,
                2,
                201,
                new DateTime(
                    2026,
                    8,
                    5),
                new DateTime(
                    2026,
                    8,
                    8));


            // ==========================================
            // DISPLAY RESERVATIONS
            // ==========================================

            Console.WriteLine();

            hotel.DisplayAllReservations();


            // ==========================================
            // DISPLAY UPDATED ROOM AVAILABILITY
            // ==========================================

            Console.WriteLine(
                "\n========== UPDATED ROOM AVAILABILITY ==========");

            hotel.DisplayAvailableRooms();


            // ==========================================
            // FIND RESERVATIONS BY GUEST
            // ==========================================

            Console.WriteLine(
                "\n========== GUEST RESERVATIONS ==========");

            List<Reservation> guestReservations =
                hotel.GetReservationsByGuest(1);

            foreach (
                Reservation reservation
                in guestReservations)
            {
                reservation.Display();
            }


            // ==========================================
            // CANCEL RESERVATION
            // ==========================================

            Console.WriteLine(
                "\n========== CANCELLING RESERVATION ==========");

            hotel.CancelReservation(1001);


            // ==========================================
            // DISPLAY FINAL ROOM AVAILABILITY
            // ==========================================

            Console.WriteLine(
                "\n========== FINAL ROOM AVAILABILITY ==========");

            hotel.DisplayAvailableRooms();


            // ==========================================
            // DISPLAY FINAL RESERVATIONS
            // ==========================================

            Console.WriteLine(
                "\n========== FINAL RESERVATIONS ==========");

            hotel.DisplayAllReservations();
        }
    }
}