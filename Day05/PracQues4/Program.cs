using System;
using System.Collections.Generic;
using System.Linq;

class Package
{
    public int PackageId;
    public string Customer;
    public string Destination;
    public double Weight;
    public string Status;
}

class Program
{
    // Fast lookup by Package ID
    static Dictionary<int, Package> packageLookup = new Dictionary<int, Package>();

    // Packages sorted by destination
    static SortedDictionary<string, List<Package>> destinationMap =
        new SortedDictionary<string, List<Package>>();

    // Packages sorted by weight
    static SortedList<double, List<Package>> weightMap =
        new SortedList<double, List<Package>>();

    // Delivery history
    static List<Package> deliveryHistory = new List<Package>();

    static void AddPackage(Package p)
    {
        packageLookup[p.PackageId] = p;

        if (!destinationMap.ContainsKey(p.Destination))
            destinationMap[p.Destination] = new List<Package>();
        destinationMap[p.Destination].Add(p);

        if (!weightMap.ContainsKey(p.Weight))
            weightMap[p.Weight] = new List<Package>();
        weightMap[p.Weight].Add(p);
    }

    static void DeliverPackage(int id)
    {
        if (packageLookup.ContainsKey(id))
        {
            packageLookup[id].Status = "Delivered";
            deliveryHistory.Add(packageLookup[id]);
        }
    }

    static void SearchPackage(int id)
    {
        if (packageLookup.ContainsKey(id))
        {
            var p = packageLookup[id];
            Console.WriteLine($"Package ID : {p.PackageId}");
            Console.WriteLine($"Customer   : {p.Customer}");
            Console.WriteLine($"Destination: {p.Destination}");
            Console.WriteLine($"Weight     : {p.Weight} kg");
            Console.WriteLine($"Status     : {p.Status}");
        }
    }

    static void Main()
    {
        AddPackage(new Package
        {
            PackageId = 101,
            Customer = "Ravi",
            Destination = "Delhi",
            Weight = 5.5,
            Status = "In Transit"
        });

        AddPackage(new Package
        {
            PackageId = 102,
            Customer = "Suraj",
            Destination = "Mumbai",
            Weight = 2.3,
            Status = "In Transit"
        });

        AddPackage(new Package
        {
            PackageId = 103,
            Customer = "Ananya",
            Destination = "Delhi",
            Weight = 8.0,
            Status = "In Transit"
        });

        Console.WriteLine("Package Lookup:");
        SearchPackage(102);

        DeliverPackage(102);

        Console.WriteLine("\nPackages by Destination:");
        foreach (var dest in destinationMap)
        {
            Console.WriteLine(dest.Key);
            foreach (var p in dest.Value)
                Console.WriteLine($"  {p.PackageId} - {p.Customer}");
        }

        Console.WriteLine("\nPackages by Weight:");
        foreach (var wt in weightMap)
        {
            foreach (var p in wt.Value)
                Console.WriteLine($"{p.PackageId} - {p.Weight} kg");
        }

        Console.WriteLine("\nDelivery History:");
        foreach (var p in deliveryHistory)
            Console.WriteLine($"{p.PackageId} - {p.Customer} - {p.Status}");
    }
}