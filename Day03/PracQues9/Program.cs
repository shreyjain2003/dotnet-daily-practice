using System;
using System.Collections.Generic;
using System.Linq;

namespace PracQues9
{
    // Represents a participant who can enroll in one or more events.
    public class Participant
    {
        public int ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantType { get; set; }

        public Participant(
            int participantId,
            string participantName,
            string participantType)
        {
            ParticipantId = participantId;
            ParticipantName = participantName;
            ParticipantType = participantType;
        }

        // Displays participant details.
        public void Display()
        {
            Console.WriteLine(
                $"Participant ID: {ParticipantId} | " +
                $"Name: {ParticipantName} | " +
                $"Type: {ParticipantType}");
        }
    }


    // Represents an event organized during the fest.
    public class FestEvent
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public decimal RegistrationFee { get; set; }

        public FestEvent(
            int eventId,
            string eventName,
            decimal registrationFee)
        {
            EventId = eventId;
            EventName = eventName;
            RegistrationFee = registrationFee;
        }

        // Displays event details.
        public void Display()
        {
            Console.WriteLine(
                $"Event ID: {EventId} | " +
                $"Event: {EventName} | " +
                $"Registration Fee: ₹{RegistrationFee:F2}");
        }
    }


    // Represents an enrollment made by a participant.
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public Participant Participant { get; set; }
        public FestEvent Event { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(
            int enrollmentId,
            Participant participant,
            FestEvent festEvent)
        {
            EnrollmentId = enrollmentId;
            Participant = participant;
            Event = festEvent;
            AmountPaid = festEvent.RegistrationFee;
            EnrollmentDate = DateTime.Now;
        }

        // Displays enrollment details.
        public void Display()
        {
            Console.WriteLine(
                $"Enrollment ID: {EnrollmentId}");

            Console.WriteLine(
                $"Participant: {Participant.ParticipantName}");

            Console.WriteLine(
                $"Event: {Event.EventName}");

            Console.WriteLine(
                $"Amount Paid: ₹{AmountPaid:F2}");

            Console.WriteLine(
                $"Date: {EnrollmentDate:dd-MM-yyyy}");

            Console.WriteLine(
                "----------------------------------------");
        }
    }


    // Manages participants, events, and enrollments.
    public class FestManagementSystem
    {
        private readonly List<Participant> participants;
        private readonly List<FestEvent> events;
        private readonly List<Enrollment> enrollments;

        public FestManagementSystem()
        {
            participants =
                new List<Participant>();

            events =
                new List<FestEvent>();

            enrollments =
                new List<Enrollment>();
        }


        // Adds a participant to the fest.
        public bool AddParticipant(
            Participant participant)
        {
            // Prevent duplicate participant IDs.
            if (participants.Any(
                p =>
                    p.ParticipantId ==
                    participant.ParticipantId))
            {
                Console.WriteLine(
                    "Error: Participant ID already exists.");

                return false;
            }

            participants.Add(
                participant);

            return true;
        }


        // Adds an event to the fest.
        public bool AddEvent(
            FestEvent festEvent)
        {
            // Prevent duplicate event IDs.
            if (events.Any(
                e =>
                    e.EventId ==
                    festEvent.EventId))
            {
                Console.WriteLine(
                    "Error: Event ID already exists.");

                return false;
            }

            events.Add(
                festEvent);

            return true;
        }


        // Finds a participant using the participant ID.
        private Participant? GetParticipantById(
            int participantId)
        {
            return participants.FirstOrDefault(
                participant =>
                    participant.ParticipantId ==
                    participantId);
        }


        // Finds an event using the event ID.
        private FestEvent? GetEventById(
            int eventId)
        {
            return events.FirstOrDefault(
                festEvent =>
                    festEvent.EventId ==
                    eventId);
        }


        // Enrolls a participant in an event.
        public bool EnrollParticipant(
            int enrollmentId,
            int participantId,
            int eventId)
        {
            // Find participant.
            Participant? participant =
                GetParticipantById(
                    participantId);

            if (participant == null)
            {
                Console.WriteLine(
                    "Error: Participant not found.");

                return false;
            }


            // Find event.
            FestEvent? festEvent =
                GetEventById(
                    eventId);

            if (festEvent == null)
            {
                Console.WriteLine(
                    "Error: Event not found.");

                return false;
            }


            // Prevent duplicate enrollment IDs.
            if (enrollments.Any(
                enrollment =>
                    enrollment.EnrollmentId ==
                    enrollmentId))
            {
                Console.WriteLine(
                    "Error: Enrollment ID already exists.");

                return false;
            }


            // Prevent the same participant from
            // registering for the same event twice.
            bool alreadyRegistered =
                enrollments.Any(
                    enrollment =>
                        enrollment.Participant.ParticipantId ==
                            participantId
                        &&
                        enrollment.Event.EventId ==
                            eventId);

            if (alreadyRegistered)
            {
                Console.WriteLine(
                    "Error: Participant is already registered for this event.");

                return false;
            }


            // Create enrollment record.
            Enrollment enrollmentRecord =
                new Enrollment(
                    enrollmentId,
                    participant,
                    festEvent);


            // Store enrollment.
            enrollments.Add(
                enrollmentRecord);

            Console.WriteLine(
                $"{participant.ParticipantName} successfully enrolled in " +
                $"{festEvent.EventName}.");

            return true;
        }


        // Calculates the total amount collected from all enrollments.
        public decimal GetTotalAmountCollected()
        {
            return enrollments.Sum(
                enrollment =>
                    enrollment.AmountPaid);
        }


        // Returns the number of participants registered
        // for a particular event.
        public int GetParticipantCountForEvent(
            int eventId)
        {
            return enrollments.Count(
                enrollment =>
                    enrollment.Event.EventId ==
                    eventId);
        }


        // Calculates the total amount collected
        // for a particular event.
        public decimal GetAmountCollectedForEvent(
            int eventId)
        {
            return enrollments
                .Where(
                    enrollment =>
                        enrollment.Event.EventId ==
                        eventId)
                .Sum(
                    enrollment =>
                        enrollment.AmountPaid);
        }


        // Displays the number of participants for every event.
        public void DisplayParticipantCountByEvent()
        {
            Console.WriteLine(
                "========== PARTICIPANTS PER EVENT ==========");

            foreach (FestEvent festEvent in events)
            {
                int participantCount =
                    GetParticipantCountForEvent(
                        festEvent.EventId);

                Console.WriteLine(
                    $"{festEvent.EventName}: " +
                    $"{participantCount} participant(s)");
            }
        }


        // Displays the amount collected for every event.
        public void DisplayAmountCollectedByEvent()
        {
            Console.WriteLine(
                "========== AMOUNT COLLECTED PER EVENT ==========");

            foreach (FestEvent festEvent in events)
            {
                decimal amountCollected =
                    GetAmountCollectedForEvent(
                        festEvent.EventId);

                Console.WriteLine(
                    $"{festEvent.EventName}: " +
                    $"₹{amountCollected:F2}");
            }
        }


        // Displays the total amount collected from the fest.
        public void DisplayTotalAmountCollected()
        {
            decimal totalAmount =
                GetTotalAmountCollected();

            Console.WriteLine(
                "========== TOTAL AMOUNT COLLECTED ==========");

            Console.WriteLine(
                $"Total Amount Collected: ₹{totalAmount:F2}");
        }


        // Displays all enrollment records.
        public void DisplayAllEnrollments()
        {
            Console.WriteLine(
                "========== ALL ENROLLMENTS ==========");

            if (enrollments.Count == 0)
            {
                Console.WriteLine(
                    "No enrollment records found.");

                return;
            }

            foreach (
                Enrollment enrollment
                in enrollments)
            {
                enrollment.Display();
            }
        }
    }


    // Application entry point.
    public class Program
    {
        public static void Main(string[] args)
        {
            // ==========================================
            // CREATE FEST MANAGEMENT SYSTEM
            // ==========================================

            FestManagementSystem fest =
                new FestManagementSystem();


            // ==========================================
            // ADD FEST EVENTS
            // ==========================================

            fest.AddEvent(
                new FestEvent(
                    101,
                    "Coding Competition",
                    500.00m));

            fest.AddEvent(
                new FestEvent(
                    102,
                    "Dance Competition",
                    300.00m));

            fest.AddEvent(
                new FestEvent(
                    103,
                    "Singing Competition",
                    250.00m));

            fest.AddEvent(
                new FestEvent(
                    104,
                    "Quiz Competition",
                    200.00m));


            // ==========================================
            // ADD PARTICIPANTS
            // ==========================================

            fest.AddParticipant(
                new Participant(
                    1,
                    "Shrey Jain",
                    "Individual"));

            fest.AddParticipant(
                new Participant(
                    2,
                    "Rahul Sharma",
                    "Individual"));

            fest.AddParticipant(
                new Participant(
                    3,
                    "Team Alpha",
                    "Team"));

            fest.AddParticipant(
                new Participant(
                    4,
                    "Team Warriors",
                    "Team"));


            // ==========================================
            // PARTICIPANT ENROLLMENTS
            // ==========================================

            Console.WriteLine(
                "========== EVENT ENROLLMENTS ==========");

            // Shrey participates in Coding.
            fest.EnrollParticipant(
                1001,
                1,
                101);

            // Shrey participates in Quiz.
            fest.EnrollParticipant(
                1002,
                1,
                104);

            // Rahul participates in Dance.
            fest.EnrollParticipant(
                1003,
                2,
                102);

            // Team Alpha participates in Coding.
            fest.EnrollParticipant(
                1004,
                3,
                101);

            // Team Alpha participates in Singing.
            fest.EnrollParticipant(
                1005,
                3,
                103);

            // Team Warriors participates in Dance.
            fest.EnrollParticipant(
                1006,
                4,
                102);


            // ==========================================
            // DISPLAY ALL ENROLLMENTS
            // ==========================================

            Console.WriteLine();

            fest.DisplayAllEnrollments();


            // ==========================================
            // DISPLAY TOTAL AMOUNT COLLECTED
            // ==========================================

            Console.WriteLine();

            fest.DisplayTotalAmountCollected();


            // ==========================================
            // DISPLAY PARTICIPANT COUNT FOR EACH EVENT
            // ==========================================

            Console.WriteLine();

            fest.DisplayParticipantCountByEvent();


            // ==========================================
            // DISPLAY AMOUNT COLLECTED FOR EACH EVENT
            // ==========================================

            Console.WriteLine();

            fest.DisplayAmountCollectedByEvent();
        }
    }
}