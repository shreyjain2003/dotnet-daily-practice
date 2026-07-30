using System;
using System.Collections.Generic;
using System.Linq;

namespace PracQues7
{
    // Represents a shoe available in the collection house.
    public class Shoe
    {
        public int ShoeId { get; set; }
        public string Brand { get; set; }
        public string ShoeType { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Shoe(
            int shoeId,
            string brand,
            string shoeType,
            string size,
            decimal price,
            int quantity)
        {
            ShoeId = shoeId;
            Brand = brand;
            ShoeType = shoeType;
            Size = size;
            Price = price;
            Quantity = quantity;
        }

        // Displays shoe details.
        public void Display()
        {
            Console.WriteLine($"Shoe ID   : {ShoeId}");
            Console.WriteLine($"Brand     : {Brand}");
            Console.WriteLine($"Shoe Type : {ShoeType}");
            Console.WriteLine($"Size      : {Size}");
            Console.WriteLine($"Price     : ₹{Price:F2}");
            Console.WriteLine($"Quantity  : {Quantity}");
            Console.WriteLine("----------------------------------------");
        }
    }


    // Represents a customer of the collection house.
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }

        public Customer(
            int customerId,
            string customerName,
            string phoneNumber)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            PhoneNumber = phoneNumber;
        }

        // Displays customer details.
        public void Display()
        {
            Console.WriteLine($"Customer ID : {CustomerId}");
            Console.WriteLine($"Name        : {CustomerName}");
            Console.WriteLine($"Phone       : {PhoneNumber}");
            Console.WriteLine("----------------------------------------");
        }
    }


    // Represents a buy or replace transaction.
    public class Transaction
    {
        public int TransactionId { get; set; }
        public Customer Customer { get; set; }
        public Shoe Shoe { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transaction(
            int transactionId,
            Customer customer,
            Shoe shoe,
            string transactionType)
        {
            TransactionId = transactionId;
            Customer = customer;
            Shoe = shoe;
            TransactionType = transactionType;
            TransactionDate = DateTime.Now;
        }

        // Displays transaction details.
        public void Display()
        {
            Console.WriteLine(
                $"Transaction ID : {TransactionId}");

            Console.WriteLine(
                $"Customer       : {Customer.CustomerName}");

            Console.WriteLine(
                $"Shoe           : {Shoe.Brand} {Shoe.ShoeType}");

            Console.WriteLine(
                $"Size           : {Shoe.Size}");

            Console.WriteLine(
                $"Transaction    : {TransactionType}");

            Console.WriteLine(
                $"Date           : {TransactionDate:dd-MM-yyyy HH:mm}");

            Console.WriteLine("----------------------------------------");
        }
    }


    // Manages shoes, customers, and transactions.
    public class ShoeCollectionHouse
    {
        private readonly List<Shoe> shoes;
        private readonly List<Customer> customers;
        private readonly List<Transaction> transactions;

        public ShoeCollectionHouse()
        {
            shoes = new List<Shoe>();
            customers = new List<Customer>();
            transactions = new List<Transaction>();
        }


        // Adds a shoe to the collection.
        public void AddShoe(Shoe shoe)
        {
            shoes.Add(shoe);
        }


        // Registers a customer.
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }


        // Finds a shoe using its ID.
        private Shoe? GetShoeById(int shoeId)
        {
            return shoes.FirstOrDefault(
                shoe => shoe.ShoeId == shoeId);
        }


        // Finds a customer using their ID.
        private Customer? GetCustomerById(int customerId)
        {
            return customers.FirstOrDefault(
                customer => customer.CustomerId == customerId);
        }


        // Customer buys a shoe.
        public bool BuyShoe(
            int transactionId,
            int customerId,
            int shoeId)
        {
            // Find customer.
            Customer? customer =
                GetCustomerById(customerId);

            if (customer == null)
            {
                Console.WriteLine(
                    "Error: Customer not found.");

                return false;
            }


            // Find shoe.
            Shoe? shoe =
                GetShoeById(shoeId);

            if (shoe == null)
            {
                Console.WriteLine(
                    "Error: Shoe not found.");

                return false;
            }


            // Check shoe availability.
            if (shoe.Quantity <= 0)
            {
                Console.WriteLine(
                    "Error: Shoe is out of stock.");

                return false;
            }


            // Prevent duplicate transaction IDs.
            if (transactions.Any(
                transaction =>
                    transaction.TransactionId ==
                    transactionId))
            {
                Console.WriteLine(
                    "Error: Transaction ID already exists.");

                return false;
            }


            // Reduce available shoe quantity.
            shoe.Quantity--;


            // Create purchase transaction.
            Transaction transactionRecord =
                new Transaction(
                    transactionId,
                    customer,
                    shoe,
                    "BUY");


            // Store transaction.
            transactions.Add(
                transactionRecord);

            Console.WriteLine(
                "Shoe purchased successfully.");

            return true;
        }


        // Customer replaces an old shoe with another shoe.
        public bool ReplaceShoe(
            int transactionId,
            int customerId,
            int oldShoeId,
            int newShoeId)
        {
            // Find customer.
            Customer? customer =
                GetCustomerById(customerId);

            if (customer == null)
            {
                Console.WriteLine(
                    "Error: Customer not found.");

                return false;
            }


            // Find old shoe.
            Shoe? oldShoe =
                GetShoeById(oldShoeId);

            if (oldShoe == null)
            {
                Console.WriteLine(
                    "Error: Old shoe not found.");

                return false;
            }


            // Find replacement shoe.
            Shoe? newShoe =
                GetShoeById(newShoeId);

            if (newShoe == null)
            {
                Console.WriteLine(
                    "Error: Replacement shoe not found.");

                return false;
            }


            // Check replacement shoe availability.
            if (newShoe.Quantity <= 0)
            {
                Console.WriteLine(
                    "Error: Replacement shoe is out of stock.");

                return false;
            }


            // Check whether customer previously bought
            // the old shoe.
            bool hasPurchasedOldShoe =
                transactions.Any(
                    transaction =>
                        transaction.Customer.CustomerId ==
                            customerId
                        &&
                        transaction.Shoe.ShoeId ==
                            oldShoeId
                        &&
                        transaction.TransactionType ==
                            "BUY");

            if (!hasPurchasedOldShoe)
            {
                Console.WriteLine(
                    "Error: Customer has not purchased this shoe.");

                return false;
            }


            // Prevent duplicate transaction IDs.
            if (transactions.Any(
                transaction =>
                    transaction.TransactionId ==
                    transactionId))
            {
                Console.WriteLine(
                    "Error: Transaction ID already exists.");

                return false;
            }


            // Return old shoe to stock.
            oldShoe.Quantity++;


            // Remove replacement shoe from stock.
            newShoe.Quantity--;


            // Create replacement transaction.
            Transaction transactionRecord =
                new Transaction(
                    transactionId,
                    customer,
                    newShoe,
                    "REPLACE");


            // Store replacement transaction.
            transactions.Add(
                transactionRecord);

            Console.WriteLine(
                "Shoe replaced successfully.");

            return true;
        }


        // Displays the number of shoes available for each kind.
        public void DisplayShoeCountByType()
        {
            Console.WriteLine(
                "========== SHOE COUNT BY TYPE ==========");

            var shoeCounts =
                shoes
                    .GroupBy(
                        shoe => shoe.ShoeType)
                    .Select(
                        group => new
                        {
                            ShoeType = group.Key,
                            TotalQuantity =
                                group.Sum(
                                    shoe => shoe.Quantity)
                        });

            foreach (var item in shoeCounts)
            {
                Console.WriteLine(
                    $"{item.ShoeType} : {item.TotalQuantity}");
            }
        }


        // Displays transaction history of a customer.
        public void DisplayCustomerTransactionHistory(
            int customerId)
        {
            Console.WriteLine(
                "========== CUSTOMER TRANSACTION HISTORY ==========");

            List<Transaction> customerTransactions =
                transactions
                    .Where(
                        transaction =>
                            transaction.Customer.CustomerId ==
                            customerId)
                    .ToList();

            if (customerTransactions.Count == 0)
            {
                Console.WriteLine(
                    "No transaction history found.");

                return;
            }

            foreach (
                Transaction transaction
                in customerTransactions)
            {
                transaction.Display();
            }
        }


        // Displays all customers who bought a particular shoe.
        public void DisplayCustomersByShoe(
            int shoeId)
        {
            Console.WriteLine(
                "========== CUSTOMERS WHO BOUGHT SHOE ==========");

            var customersWhoBought =
                transactions
                    .Where(
                        transaction =>
                            transaction.Shoe.ShoeId ==
                            shoeId
                        &&
                        (
                            transaction.TransactionType ==
                            "BUY"
                            ||
                            transaction.TransactionType ==
                            "REPLACE"
                        ))
                    .Select(
                        transaction =>
                            transaction.Customer)
                    .DistinctBy(
                        customer =>
                            customer.CustomerId)
                    .ToList();

            if (customersWhoBought.Count == 0)
            {
                Console.WriteLine(
                    "No customer has bought this shoe.");

                return;
            }

            foreach (
                Customer customer
                in customersWhoBought)
            {
                customer.Display();
            }
        }


        // Displays all shoes in the collection house.
        public void DisplayAllShoes()
        {
            Console.WriteLine(
                "========== SHOE COLLECTION ==========");

            foreach (Shoe shoe in shoes)
            {
                shoe.Display();
            }
        }


        // Displays all transactions.
        public void DisplayAllTransactions()
        {
            Console.WriteLine(
                "========== ALL TRANSACTIONS ==========");

            foreach (
                Transaction transaction
                in transactions)
            {
                transaction.Display();
            }
        }
    }


    // Application entry point.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create shoe collection house.
            ShoeCollectionHouse house =
                new ShoeCollectionHouse();


            // ==========================================
            // ADD SHOES
            // ==========================================

            house.AddShoe(
                new Shoe(
                    101,
                    "Nike",
                    "Running",
                    "9",
                    5000.00m,
                    5));

            house.AddShoe(
                new Shoe(
                    102,
                    "Adidas",
                    "Running",
                    "10",
                    4500.00m,
                    3));

            house.AddShoe(
                new Shoe(
                    103,
                    "Puma",
                    "Casual",
                    "9",
                    3500.00m,
                    4));

            house.AddShoe(
                new Shoe(
                    104,
                    "Nike",
                    "Casual",
                    "10",
                    4000.00m,
                    2));


            // ==========================================
            // ADD CUSTOMERS
            // ==========================================

            house.AddCustomer(
                new Customer(
                    1,
                    "Shrey Jain",
                    "9876543210"));

            house.AddCustomer(
                new Customer(
                    2,
                    "Rahul Sharma",
                    "9876501234"));

            house.AddCustomer(
                new Customer(
                    3,
                    "Priya Verma",
                    "9876512345"));


            // ==========================================
            // DISPLAY SHOE COLLECTION
            // ==========================================

            house.DisplayAllShoes();


            // ==========================================
            // DISPLAY NUMBER OF SHOES BY KIND
            // ==========================================

            Console.WriteLine();

            house.DisplayShoeCountByType();


            // ==========================================
            // CUSTOMER PURCHASES SHOES
            // ==========================================

            Console.WriteLine(
                "\n========== SHOE PURCHASES ==========");

            house.BuyShoe(
                1001,
                1,
                101);

            house.BuyShoe(
                1002,
                2,
                101);

            house.BuyShoe(
                1003,
                3,
                103);


            // ==========================================
            // CUSTOMER REPLACES A SHOE
            // ==========================================

            Console.WriteLine(
                "\n========== SHOE REPLACEMENT ==========");

            house.ReplaceShoe(
                1004,
                1,
                101,
                104);


            // ==========================================
            // DISPLAY CUSTOMER TRANSACTION HISTORY
            // ==========================================

            Console.WriteLine();

            house.DisplayCustomerTransactionHistory(
                1);


            // ==========================================
            // DISPLAY CUSTOMERS WHO BOUGHT A SHOE
            // ==========================================

            Console.WriteLine();

            house.DisplayCustomersByShoe(
                101);


            // ==========================================
            // DISPLAY UPDATED SHOE COUNT
            // ==========================================

            Console.WriteLine();

            house.DisplayShoeCountByType();


            // ==========================================
            // DISPLAY ALL TRANSACTIONS
            // ==========================================

            Console.WriteLine();

            house.DisplayAllTransactions();
        }
    }
}