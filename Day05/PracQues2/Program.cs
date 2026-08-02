using System;
using System.Collections.Generic;
using System.Linq;

class Order
{
    public int OrderId { get; set; }
    public string Trader { get; set; }
    public string Type { get; set; }      // BUY / SELL
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime Time { get; set; }
}

class Program
{
    static Dictionary<int, Order> orderLookup = new Dictionary<int, Order>();

    static SortedDictionary<decimal, Queue<Order>> buyOrders =
        new SortedDictionary<decimal, Queue<Order>>(Comparer<decimal>.Create((x, y) => y.CompareTo(x)));

    static SortedDictionary<decimal, Queue<Order>> sellOrders =
        new SortedDictionary<decimal, Queue<Order>>();

    static SortedList<decimal, List<Order>> stopLossOrders =
        new SortedList<decimal, List<Order>>();

    static List<Order> executionHistory = new List<Order>();

    static void AddOrder(Order order)
    {
        orderLookup[order.OrderId] = order;

        if (order.Type == "BUY")
        {
            if (!buyOrders.ContainsKey(order.Price))
                buyOrders[order.Price] = new Queue<Order>();

            buyOrders[order.Price].Enqueue(order);
        }
        else
        {
            if (!sellOrders.ContainsKey(order.Price))
                sellOrders[order.Price] = new Queue<Order>();

            sellOrders[order.Price].Enqueue(order);
        }
    }

    static void AddStopLoss(Order order, decimal triggerPrice)
    {
        if (!stopLossOrders.ContainsKey(triggerPrice))
            stopLossOrders[triggerPrice] = new List<Order>();

        stopLossOrders[triggerPrice].Add(order);
    }

    static void TriggerStopLoss(decimal marketPrice)
    {
        Console.WriteLine("\nTriggered Stop Loss Orders:");

        foreach (var level in stopLossOrders.ToList())
        {
            if (level.Key <= marketPrice)
            {
                foreach (var order in level.Value)
                {
                    Console.WriteLine($"Executed Order {order.OrderId} at {level.Key}");
                    executionHistory.Add(order);
                }

                stopLossOrders.Remove(level.Key);
            }
        }
    }

    static void ShowMarketDepth()
    {
        Console.WriteLine("\nTop Buy Orders");
        foreach (var level in buyOrders.Take(10))
            Console.WriteLine($"{level.Key} -> Qty {level.Value.Sum(x => x.Quantity)}");

        Console.WriteLine("\nTop Sell Orders");
        foreach (var level in sellOrders.Take(10))
            Console.WriteLine($"{level.Key} -> Qty {level.Value.Sum(x => x.Quantity)}");
    }

    static void Main()
    {
        AddOrder(new Order
        {
            OrderId = 101,
            Trader = "Ravi",
            Type = "BUY",
            Price = 100,
            Quantity = 50,
            Time = DateTime.Now
        });

        AddOrder(new Order
        {
            OrderId = 102,
            Trader = "Ananya",
            Type = "BUY",
            Price = 99,
            Quantity = 40,
            Time = DateTime.Now
        });

        AddOrder(new Order
        {
            OrderId = 201,
            Trader = "Rahul",
            Type = "SELL",
            Price = 101,
            Quantity = 30,
            Time = DateTime.Now
        });

        AddOrder(new Order
        {
            OrderId = 202,
            Trader = "Karan",
            Type = "SELL",
            Price = 102,
            Quantity = 60,
            Time = DateTime.Now
        });

        AddStopLoss(new Order
        {
            OrderId = 301,
            Trader = "Sneha",
            Type = "SELL",
            Price = 98,
            Quantity = 25,
            Time = DateTime.Now
        }, 100);

        Console.WriteLine("Instant Lookup:");
        Console.WriteLine(orderLookup[101].Trader);

        ShowMarketDepth();

        TriggerStopLoss(100);

        Console.WriteLine("\nExecution History:");
        foreach (var order in executionHistory)
            Console.WriteLine($"{order.OrderId} - {order.Trader}");
    }
}