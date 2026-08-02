using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public int Id;
    public string Name;
    public string Category;
    public decimal Price;
    public double Rating;
    public int Stock;
    public int Popularity;
}

class Program
{
    // Fast lookup
    static Dictionary<int, Product> productCatalog = new Dictionary<int, Product>();

    // Products by price
    static SortedDictionary<decimal, List<Product>> productsByPrice =
        new SortedDictionary<decimal, List<Product>>();

    // Products by rating
    static SortedList<double, List<Product>> productsByRating =
        new SortedList<double, List<Product>>();

    // User browsing history
    static Dictionary<string, List<Product>> browsingHistory =
        new Dictionary<string, List<Product>>();

    // Shopping cart
    static Dictionary<string, List<Product>> shoppingCart =
        new Dictionary<string, List<Product>>();

    static void AddProduct(Product p)
    {
        productCatalog[p.Id] = p;

        if (!productsByPrice.ContainsKey(p.Price))
            productsByPrice[p.Price] = new List<Product>();
        productsByPrice[p.Price].Add(p);

        if (!productsByRating.ContainsKey(p.Rating))
            productsByRating[p.Rating] = new List<Product>();
        productsByRating[p.Rating].Add(p);
    }

    static void BrowseProduct(string user, int productId)
    {
        if (!browsingHistory.ContainsKey(user))
            browsingHistory[user] = new List<Product>();

        browsingHistory[user].Add(productCatalog[productId]);
    }

    static void AddToCart(string user, int productId)
    {
        if (!shoppingCart.ContainsKey(user))
            shoppingCart[user] = new List<Product>();

        shoppingCart[user].Add(productCatalog[productId]);
    }

    static void RecommendProducts(string user)
    {
        Console.WriteLine($"\nRecommendations for {user}:");

        if (!browsingHistory.ContainsKey(user))
            return;

        string lastCategory = browsingHistory[user].Last().Category;

        var recommendations = productCatalog.Values
            .Where(p => p.Category == lastCategory && p.Stock > 0)
            .OrderByDescending(p => p.Rating)
            .ThenBy(p => p.Price);

        foreach (var p in recommendations)
            Console.WriteLine($"{p.Name} | ₹{p.Price} | Rating {p.Rating}");
    }

    static void Main()
    {
        AddProduct(new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 60000, Rating = 4.8, Stock = 5, Popularity = 95 });
        AddProduct(new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 800, Rating = 4.5, Stock = 20, Popularity = 80 });
        AddProduct(new Product { Id = 3, Name = "Keyboard", Category = "Electronics", Price = 1500, Rating = 4.6, Stock = 10, Popularity = 85 });
        AddProduct(new Product { Id = 4, Name = "Novel", Category = "Books", Price = 500, Rating = 4.9, Stock = 30, Popularity = 70 });

        BrowseProduct("Ravi", 1);
        BrowseProduct("Ravi", 2);

        AddToCart("Ravi", 2);

        RecommendProducts("Ravi");

        Console.WriteLine("\nProducts Sorted by Price:");
        foreach (var item in productsByPrice)
            foreach (var p in item.Value)
                Console.WriteLine($"{p.Name} - ₹{p.Price}");

        Console.WriteLine("\nProducts Sorted by Rating:");
        foreach (var item in productsByRating.Reverse())
            foreach (var p in item.Value)
                Console.WriteLine($"{p.Name} - {p.Rating}");
    }
}