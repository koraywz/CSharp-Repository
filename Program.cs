using System;
using System.Collections.Generic;
using System.Linq;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Orders { get; set; }
}

public class Orders
{
    public int UserId { get; set; }
    public string Product { get; set; }
}

public class Addition
{
    public int A { get; set; }
    public int B { get; set; }

    private Addition(int a, int b)
    {
        A = a;
        B = b;
    }

    public static Addition Create(int a, int b)
    {
        var t = new Addition(a, b);
        Console.WriteLine($"Sum: {t.A + t.B}");
        return t;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // User and Orders example - nested list
        var usersWithOrders = new List<User>
        {
            new User { Name = "Koray", Orders = new List<string> { "Laptop", "Mouse" } },
            new User { Name = "Mamut", Orders = new List<string> { "Keyboard", "Monitor" } }
        };

        var orderLists = usersWithOrders.Select(x => x.Orders);
        foreach (var orderList in orderLists)
        {
            foreach (var order in orderList)
            {
                Console.WriteLine(order);
            }
        }

        // User and Orders example - join operation
        var users = new List<User>
        {
            new User { Id = 5, Name = "Koray" },
            new User { Id = 9, Name = "Mamut" }
        };

        var orders = new List<Orders>
        {
            new Orders { UserId = 5, Product = "Laptop" },
            new Orders { UserId = 5, Product = "Keyboard" },
            new Orders { UserId = 9, Product = "Mouse" }
        };

        var result = users.Join(
            orders,
            u => u.Id,
            o => o.UserId,
            (u, o) => new { u.Name, o.Product }
        );

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Name} - {item.Product}");
        }

        // Addition example
        var t = Addition.Create(5, 7); // Output: Sum: 12
        Console.WriteLine(t.A); // 5
        Console.WriteLine(t.B); // 7

        // String and Span example
        string sentence = "Hello World";

        // Old way: creates a new string (Allocation!)
        string word = sentence.Substring(0, 5);
        Console.WriteLine("_" + word + "_"); // "_Hello_"

        // Using Span: only a window (Zero Allocation!)
        ReadOnlySpan<char> spanWord = sentence.AsSpan().Slice(0, 5);
        Console.WriteLine("_" + spanWord.ToString() + "_"); // "_Hello_"
    }
}