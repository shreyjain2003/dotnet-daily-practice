using System;
using System.Collections.Generic;
using System.Linq;

class Player
{
    public int PlayerId;
    public string Name;
    public int Level;
    public int Wealth;
    public int PvPRating;
}

class Program
{
    // Fast lookup
    static Dictionary<int, Player> players = new Dictionary<int, Player>();

    // Leaderboards
    static SortedDictionary<int, List<Player>> levelBoard =
        new SortedDictionary<int, List<Player>>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

    static SortedDictionary<int, List<Player>> wealthBoard =
        new SortedDictionary<int, List<Player>>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

    // Guild ranking
    static SortedList<int, string> guildRanking = new SortedList<int, string>();

    // Inventory
    static Dictionary<int, List<string>> inventory =
        new Dictionary<int, List<string>>();

    // Auction House
    static SortedDictionary<int, List<string>> auctionHouse =
        new SortedDictionary<int, List<string>>();

    static void AddPlayer(Player p)
    {
        players[p.PlayerId] = p;

        if (!levelBoard.ContainsKey(p.Level))
            levelBoard[p.Level] = new List<Player>();
        levelBoard[p.Level].Add(p);

        if (!wealthBoard.ContainsKey(p.Wealth))
            wealthBoard[p.Wealth] = new List<Player>();
        wealthBoard[p.Wealth].Add(p);

        inventory[p.PlayerId] = new List<string>();
    }

    static void Main()
    {
        AddPlayer(new Player { PlayerId = 1, Name = "Ravi", Level = 50, Wealth = 50000, PvPRating = 1800 });
        AddPlayer(new Player { PlayerId = 2, Name = "Ananya", Level = 65, Wealth = 90000, PvPRating = 2200 });
        AddPlayer(new Player { PlayerId = 3, Name = "Rahul", Level = 40, Wealth = 35000, PvPRating = 1600 });

        guildRanking.Add(100, "Dragon Guild");
        guildRanking.Add(200, "Phoenix Guild");
        guildRanking.Add(150, "Titan Guild");

        inventory[1].Add("Sword");
        inventory[1].Add("Shield");
        inventory[2].Add("Bow");

        auctionHouse[1000] = new List<string> { "Iron Sword" };
        auctionHouse[5000] = new List<string> { "Magic Staff" };
        auctionHouse[2500] = new List<string> { "Golden Shield" };

        Console.WriteLine("=== Player Lookup ===");
        Console.WriteLine(players[2].Name);

        Console.WriteLine("\n=== Level Leaderboard ===");
        foreach (var level in levelBoard)
            foreach (var p in level.Value)
                Console.WriteLine($"{p.Name} - Level {p.Level}");

        Console.WriteLine("\n=== Wealth Leaderboard ===");
        foreach (var wealth in wealthBoard)
            foreach (var p in wealth.Value)
                Console.WriteLine($"{p.Name} - {p.Wealth}");

        Console.WriteLine("\n=== Guild Ranking ===");
        foreach (var g in guildRanking.Reverse())
            Console.WriteLine($"{g.Value} : {g.Key}");

        Console.WriteLine("\n=== Ravi Inventory ===");
        foreach (var item in inventory[1])
            Console.WriteLine(item);

        Console.WriteLine("\n=== Auction House ===");
        foreach (var item in auctionHouse)
            foreach (var product in item.Value)
                Console.WriteLine($"{product} - {item.Key}");
    }
}