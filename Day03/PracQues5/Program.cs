using System;
using System.Collections.Generic;
using System.Linq;

namespace PracQues5
{
    // Represents a patient registered in the hospital.
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MedicalCondition { get; set; }

        public Patient(
            int patientId,
            string patientName,
            int age,
            string gender,
            string medicalCondition)
        {
            PatientId = patientId;
            PatientName = patientName;
            Age = age;
            Gender = gender;
            MedicalCondition = medicalCondition;
        }

        // Displays patient details.
        public void Display()
        {
            Console.WriteLine($"Patient ID        : {PatientId}");
            Console.WriteLine($"Patient Name      : {PatientName}");
            Console.WriteLine($"Age               : {Age}");
            Console.WriteLine($"Gender            : {Gender}");
            Console.WriteLine(
                $"Medical Condition : {MedicalCondition}");
            Console.WriteLine("----------------------------------------");
        }
    }


    // Represents a surgeon working at the hospital.
    public class Surgeon
    {
        public int SurgeonId { get; set; }
        public string SurgeonName { get; set; }
        public string Specialization { get; set; }

        public Surgeon(
            int surgeonId,
            string surgeonName,
            string specialization)
        {
            SurgeonId = surgeonId;
            SurgeonName = surgeonName;
            Specialization = specialization;
        }

        // Displays surgeon details.
        public void Display()
        {
            Console.WriteLine($"Surgeon ID      : {SurgeonId}");
            Console.WriteLine($"Surgeon Name    : {SurgeonName}");
            Console.WriteLine(
                $"Specialization : {Specialization}");
            Console.WriteLine("----------------------------------------");
        }
    }


    // Represents a surgery scheduled for a patient.
    public class Surgery
    {
        public int SurgeryId { get; set; }
        public Patient Patient { get; set; }
        public Surgeon Surgeon { get; set; }
        public string SurgeryType { get; set; }
        public DateTime SurgeryDate { get; set; }

        public Surgery(
            int surgeryId,
            Patient patient,
            Surgeon surgeon,
            string surgeryType,
            DateTime surgeryDate)
        {
            SurgeryId = surgeryId;
            Patient = patient;
            Surgeon = surgeon;
            SurgeryType = surgeryType;
            SurgeryDate = surgeryDate;
        }

        // Displays surgery details.
        public void Display()
        {
            Console.WriteLine($"Surgery ID      : {SurgeryId}");
            Console.WriteLine(
                $"Patient         : {Patient.PatientName}");
            Console.WriteLine(
                $"Surgeon         : {Surgeon.SurgeonName}");
            Console.WriteLine(
                $"Surgery Type    : {SurgeryType}");
            Console.WriteLine(
                $"Surgery Date    : {SurgeryDate:dd-MM-yyyy HH:mm}");
            Console.WriteLine("----------------------------------------");
        }
    }


    // Manages patients, surgeons, and surgeries.
    public class Hospital
    {
        private readonly List<Patient> patients;
        private readonly List<Surgeon> surgeons;
        private readonly List<Surgery> surgeries;

        public Hospital()
        {
            patients = new List<Patient>();
            surgeons = new List<Surgeon>();
            surgeries = new List<Surgery>();
        }


        // Registers a new patient.
        public void AddPatient(Patient patient)
        {
            patients.Add(patient);
        }


        // Adds a surgeon to the hospital.
        public void AddSurgeon(Surgeon surgeon)
        {
            surgeons.Add(surgeon);
        }


        // Schedules a new surgery.
        public bool ScheduleSurgery(
            Surgery surgery)
        {
            // Prevent duplicate surgery IDs.
            if (surgeries.Any(
                existing =>
                    existing.SurgeryId ==
                    surgery.SurgeryId))
            {
                Console.WriteLine(
                    "Error: Surgery ID already exists.");

                return false;
            }


            // Ensure the patient is registered.
            if (!patients.Any(
                patient =>
                    patient.PatientId ==
                    surgery.Patient.PatientId))
            {
                Console.WriteLine(
                    "Error: Patient is not registered.");

                return false;
            }


            // Ensure the surgeon is available.
            bool surgeonAlreadyScheduled =
                surgeries.Any(
                    existing =>
                        existing.Surgeon.SurgeonId ==
                        surgery.Surgeon.SurgeonId
                        &&
                        existing.SurgeryDate ==
                        surgery.SurgeryDate);

            if (surgeonAlreadyScheduled)
            {
                Console.WriteLine(
                    "Error: Surgeon is already scheduled " +
                    "for another surgery at this time.");

                return false;
            }


            surgeries.Add(surgery);

            Console.WriteLine(
                "Surgery scheduled successfully.");

            return true;
        }


        // Returns all surgeries scheduled for a patient.
        public List<Surgery> GetSurgeriesByPatient(
            int patientId)
        {
            return surgeries
                .Where(
                    surgery =>
                        surgery.Patient.PatientId ==
                        patientId)
                .ToList();
        }


        // Returns all surgeries assigned to a surgeon.
        public List<Surgery> GetSurgeriesBySurgeon(
            int surgeonId)
        {
            return surgeries
                .Where(
                    surgery =>
                        surgery.Surgeon.SurgeonId ==
                        surgeonId)
                .ToList();
        }


        // Returns all surgeons with a specific specialization.
        public List<Surgeon> GetSurgeonsBySpecialization(
            string specialization)
        {
            return surgeons
                .Where(
                    surgeon =>
                        surgeon.Specialization.Equals(
                            specialization,
                            StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        // Displays all registered patients.
        public void DisplayAllPatients()
        {
            Console.WriteLine(
                "========== REGISTERED PATIENTS ==========");

            foreach (Patient patient in patients)
            {
                patient.Display();
            }
        }


        // Displays all surgeons.
        public void DisplayAllSurgeons()
        {
            Console.WriteLine(
                "========== HOSPITAL SURGEONS ==========");

            foreach (Surgeon surgeon in surgeons)
            {
                surgeon.Display();
            }
        }


        // Displays all scheduled surgeries.
        public void DisplayAllSurgeries()
        {
            Console.WriteLine(
                "========== SCHEDULED SURGERIES ==========");

            if (surgeries.Count == 0)
            {
                Console.WriteLine(
                    "No surgeries scheduled.");

                return;
            }

            foreach (Surgery surgery in surgeries)
            {
                surgery.Display();
            }
        }
    }


    // Application entry point.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create hospital management system.
            Hospital hospital =
                new Hospital();


            // ==========================================
            // REGISTER PATIENTS
            // ==========================================

            Patient patient1 =
                new Patient(
                    101,
                    "Amit Sharma",
                    45,
                    "Male",
                    "Heart Disease");

            Patient patient2 =
                new Patient(
                    102,
                    "Priya Verma",
                    32,
                    "Female",
                    "Gallbladder Disease");

            hospital.AddPatient(patient1);
            hospital.AddPatient(patient2);


            // ==========================================
            // REGISTER SURGEONS
            // ==========================================

            Surgeon surgeon1 =
                new Surgeon(
                    201,
                    "Dr. Rajesh Mehta",
                    "Cardiothoracic Surgery");

            Surgeon surgeon2 =
                new Surgeon(
                    202,
                    "Dr. Neha Kapoor",
                    "General Surgery");

            hospital.AddSurgeon(surgeon1);
            hospital.AddSurgeon(surgeon2);


            // ==========================================
            // DISPLAY PATIENTS AND SURGEONS
            // ==========================================

            hospital.DisplayAllPatients();

            Console.WriteLine();

            hospital.DisplayAllSurgeons();


            // ==========================================
            // SCHEDULE SURGERIES
            // ==========================================

            Console.WriteLine(
                "\n========== SCHEDULING SURGERIES ==========");

            hospital.ScheduleSurgery(
                new Surgery(
                    301,
                    patient1,
                    surgeon1,
                    "Heart Bypass Surgery",
                    new DateTime(
                        2026,
                        8,
                        10,
                        10,
                        00,
                        0)));


            hospital.ScheduleSurgery(
                new Surgery(
                    302,
                    patient2,
                    surgeon2,
                    "Gallbladder Removal",
                    new DateTime(
                        2026,
                        8,
                        12,
                        14,
                        00,
                        0)));


            // ==========================================
            // DISPLAY ALL SURGERIES
            // ==========================================

            Console.WriteLine();

            hospital.DisplayAllSurgeries();


            // ==========================================
            // FIND SURGERIES BY PATIENT
            // ==========================================

            Console.WriteLine(
                "\n========== PATIENT SURGERIES ==========");

            List<Surgery> patientSurgeries =
                hospital.GetSurgeriesByPatient(101);

            foreach (Surgery surgery in patientSurgeries)
            {
                surgery.Display();
            }


            // ==========================================
            // FIND SURGERIES BY SURGEON
            // ==========================================

            Console.WriteLine(
                "\n========== SURGEON SCHEDULE ==========");

            List<Surgery> surgeonSurgeries =
                hospital.GetSurgeriesBySurgeon(201);

            foreach (Surgery surgery in surgeonSurgeries)
            {
                surgery.Display();
            }


            // ==========================================
            // SEARCH SURGEONS BY SPECIALIZATION
            // ==========================================

            Console.WriteLine(
                "\n========== SURGEONS BY SPECIALIZATION ==========");

            List<Surgeon> cardiothoracicSurgeons =
                hospital.GetSurgeonsBySpecialization(
                    "Cardiothoracic Surgery");

            foreach (
                Surgeon surgeon
                in cardiothoracicSurgeons)
            {
                surgeon.Display();
            }
        }
    }
}