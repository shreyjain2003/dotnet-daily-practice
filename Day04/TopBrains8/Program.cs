using System;
using System.Collections.Generic;

class LibManagement
{
    static List<dynamic> books = new List<dynamic>();

    static void Main()
    {
        books.Add(new { Id = 1, Name = "C# Programming", Author = "John", Publisher = "Pearson", Price = 550.0 });
        books.Add(new { Id = 2, Name = "Java Basics", Author = "James", Publisher = "McGraw", Price = 450.0 });
        books.Add(new { Id = 3, Name = "Python Guide", Author = "David", Publisher = "Pearson", Price = 700.0 });

        while (true)
        {
            Console.WriteLine("\n===== BOOK LIBRARY MANAGEMENT =====");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Update Book");
            Console.WriteLine("3. Delete Book");
            Console.WriteLine("4. View All Books");
            Console.WriteLine("5. Search Book by Name");
            Console.WriteLine("6. Search Book by Publisher");
            Console.WriteLine("7. Highest Price Book");
            Console.WriteLine("8. Lowest Price Book");
            Console.WriteLine("9. Exit");
            Console.Write("Enter Choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    UpdateBook();
                    break;
                case 3:
                    DeleteBook();
                    break;
                case 4:
                    ViewBooks();
                    break;
                case 5:
                    SearchByName();
                    break;
                case 6:
                    SearchByPublisher();
                    break;
                case 7:
                    HighestPriceBook();
                    break;
                case 8:
                    LowestPriceBook();
                    break;
                case 9:
                    return;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }

    static void AddBook()
    {
        Console.Write("Book Id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Book Name: ");
        string name = Console.ReadLine();

        Console.Write("Author: ");
        string author = Console.ReadLine();

        Console.Write("Publisher: ");
        string publisher = Console.ReadLine();

        Console.Write("Price: ");
        double price = Convert.ToDouble(Console.ReadLine());

        books.Add(new
        {
            Id = id,
            Name = name,
            Author = author,
            Publisher = publisher,
            Price = price
        });

        Console.WriteLine("Book Added Successfully.");
    }

    static void UpdateBook()
    {
        Console.Write("Enter Book Id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].Id == id)
            {
                Console.Write("New Book Name: ");
                string name = Console.ReadLine();

                Console.Write("New Author: ");
                string author = Console.ReadLine();

                Console.Write("New Publisher: ");
                string publisher = Console.ReadLine();

                Console.Write("New Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                books[i] = new
                {
                    Id = id,
                    Name = name,
                    Author = author,
                    Publisher = publisher,
                    Price = price
                };

                Console.WriteLine("Book Updated Successfully.");
                return;
            }
        }

        Console.WriteLine("Book Not Found.");
    }

    static void DeleteBook()
    {
        Console.Write("Enter Book Id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].Id == id)
            {
                books.RemoveAt(i);
                Console.WriteLine("Book Deleted Successfully.");
                return;
            }
        }

        Console.WriteLine("Book Not Found.");
    }

    static void ViewBooks()
    {
        Console.WriteLine("\nID\tName\t\tAuthor\tPublisher\tPrice");

        foreach (dynamic book in books)
        {
            Console.WriteLine($"{book.Id}\t{book.Name}\t{book.Author}\t{book.Publisher}\t{book.Price}");
        }
    }

    static void SearchByName()
    {
        Console.Write("Enter Book Name: ");
        string name = Console.ReadLine().ToLower();

        foreach (dynamic book in books)
        {
            if (book.Name.ToLower().Contains(name))
            {
                Console.WriteLine($"{book.Id} {book.Name} {book.Author} {book.Publisher} {book.Price}");
            }
        }
    }

    static void SearchByPublisher()
    {
        Console.Write("Enter Publisher: ");
        string publisher = Console.ReadLine().ToLower();

        foreach (dynamic book in books)
        {
            if (book.Publisher.ToLower().Contains(publisher))
            {
                Console.WriteLine($"{book.Id} {book.Name} {book.Author} {book.Publisher} {book.Price}");
            }
        }
    }

    static void HighestPriceBook()
    {
        if (books.Count == 0)
            return;

        dynamic max = books[0];

        foreach (dynamic book in books)
        {
            if (book.Price > max.Price)
                max = book;
        }

        Console.WriteLine("\nHighest Price Book:");
        Console.WriteLine($"{max.Id} {max.Name} {max.Author} {max.Publisher} {max.Price}");
    }

    static void LowestPriceBook()
    {
        if (books.Count == 0)
            return;

        dynamic min = books[0];

        foreach (dynamic book in books)
        {
            if (book.Price < min.Price)
                min = book;
        }

        Console.WriteLine("\nLowest Price Book:");
        Console.WriteLine($"{min.Id} {min.Name} {min.Author} {min.Publisher} {min.Price}");
    }
}