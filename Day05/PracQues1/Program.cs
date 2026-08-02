//Scenario-Based Coding Problems: Collections in C#
using System;
using System.Collections.Generic;
using System.Linq;

class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Priority { get; set; }
    public int ConditionScore { get; set; }
    public DateTime AdmissionTime { get; set; }
}

class Program
{
    // O(1) lookup by Patient ID
    static Dictionary<int, Patient> patientLookup = new Dictionary<int, Patient>();

    // Sorted by Priority
    static SortedDictionary<int, List<Patient>> priorityPatients =
        new SortedDictionary<int, List<Patient>>();

    // Department-wise sorted by Condition Score
    static Dictionary<string, SortedList<int, Patient>> departmentPatients =
        new Dictionary<string, SortedList<int, Patient>>();

    // Admission Order
    static List<Patient> admissionQueue = new List<Patient>();

    // Bed Allocation (Bed Price -> Bed Number)
    static SortedList<int, string> beds = new SortedList<int, string>();

    static int GetPriorityValue(string priority)
    {
        switch (priority)
        {
            case "Critical": return 1;
            case "High": return 2;
            case "Medium": return 3;
            default: return 4;
        }
    }

    static void AddPatient(Patient p)
    {
        patientLookup[p.Id] = p;

        int key = GetPriorityValue(p.Priority);
        if (!priorityPatients.ContainsKey(key))
            priorityPatients[key] = new List<Patient>();

        priorityPatients[key].Add(p);

        if (!departmentPatients.ContainsKey(p.Department))
            departmentPatients[p.Department] = new SortedList<int, Patient>();

        while (departmentPatients[p.Department].ContainsKey(p.ConditionScore))
            p.ConditionScore++;

        departmentPatients[p.Department].Add(p.ConditionScore, p);

        admissionQueue.Add(p);
    }

    static void ChangePriority(int id, string newPriority)
    {
        if (!patientLookup.ContainsKey(id))
            return;

        Patient p = patientLookup[id];

        priorityPatients[GetPriorityValue(p.Priority)].Remove(p);

        p.Priority = newPriority;

        int key = GetPriorityValue(newPriority);
        if (!priorityPatients.ContainsKey(key))
            priorityPatients[key] = new List<Patient>();

        priorityPatients[key].Add(p);
    }

    static void ShowCriticalCardiologyPatients()
    {
        Console.WriteLine("\nCritical Cardiology Patients:");

        foreach (Patient p in patientLookup.Values)
        {
            if (p.Department == "Cardiology" &&
                p.Priority == "Critical" &&
                (DateTime.Now - p.AdmissionTime).TotalHours <= 24)
            {
                Console.WriteLine($"{p.Id} - {p.Name}");
            }
        }
    }

    static void Main()
    {
        beds.Add(5000, "B101");
        beds.Add(7000, "B102");
        beds.Add(9000, "B103");

        AddPatient(new Patient
        {
            Id = 1,
            Name = "Ravi",
            Department = "Cardiology",
            Priority = "Medium",
            ConditionScore = 70,
            AdmissionTime = DateTime.Now.AddHours(-2)
        });

        AddPatient(new Patient
        {
            Id = 2,
            Name = "Ananya",
            Department = "Neurology",
            Priority = "High",
            ConditionScore = 85,
            AdmissionTime = DateTime.Now.AddHours(-3)
        });

        AddPatient(new Patient
        {
            Id = 3,
            Name = "Rahul",
            Department = "Cardiology",
            Priority = "Critical",
            ConditionScore = 95,
            AdmissionTime = DateTime.Now.AddHours(-5)
        });

        Console.WriteLine("Patient Lookup (ID = 2):");
        Console.WriteLine(patientLookup[2].Name);

        Console.WriteLine("\nPatients by Priority:");
        foreach (var group in priorityPatients)
        {
            foreach (var p in group.Value)
                Console.WriteLine($"{p.Name} - {p.Priority}");
        }

        Console.WriteLine("\nAdmission Order:");
        foreach (var p in admissionQueue)
            Console.WriteLine(p.Name);

        Console.WriteLine("\nChanging Ravi's Priority to Critical...");
        ChangePriority(1, "Critical");

        Console.WriteLine("\nUpdated Priority List:");
        foreach (var group in priorityPatients)
        {
            foreach (var p in group.Value)
                Console.WriteLine($"{p.Name} - {p.Priority}");
        }

        ShowCriticalCardiologyPatients();

        Console.WriteLine("\nBeds in Price Range 5000-8000:");
        foreach (var bed in beds)
        {
            if (bed.Key >= 5000 && bed.Key <= 8000)
                Console.WriteLine($"{bed.Value} - Rs.{bed.Key}");
        }
    }
}