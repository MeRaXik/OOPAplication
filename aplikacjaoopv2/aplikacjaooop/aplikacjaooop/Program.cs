using System;
using System.Collections.Generic;

// Klasa reprezentująca pojedyncze zamówienie
public class Order
{
    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public decimal TotalPrice => Quantity * Price;
}

// Klasa reprezentująca system obsługi zamówień
public class OrderSystem
{
    private readonly List<Order> orders = new List<Order>();

    // Metoda dodająca nowe zamówienie
    public void AddOrder(Order order)
    {
        bool isOrderIdUnique = orders.All(o => o.OrderId != order.OrderId);
        if (isOrderIdUnique)
        {
            orders.Add(order);
            Console.WriteLine($"Zamówienie o numerze {order.OrderId} dodane.");
        }
        else
        {
            Console.WriteLine($"Zamówienie o numerze {order.OrderId} już istnieje. Nie można zduplikować numeru zamówienia.");
        }
    }

    // Metoda usuwająca zamówienie o podanym numerze
    public void RemoveOrder(int orderId)
    {
        Order order = orders.Find(o => o.OrderId == orderId);
        if (order != null)
        {
            orders.Remove(order);
            Console.WriteLine($"Zamówienie o numerze {orderId} usunięte.");
        }
        else
        {
            Console.WriteLine($"Nie znaleziono zamówienia o numerze {orderId}.");
        }
    }

    // Metoda wyświetlająca szczegóły zamówienia
    public void ShowOrderDetails(int orderId)
    {
        Order order = orders.Find(o => o.OrderId == orderId);
        if (order != null)
        {
            Console.WriteLine($"Szczegóły zamówienia o numerze {orderId}:");
            Console.WriteLine($"Produkt: {order.ProductName}");
            Console.WriteLine($"Ilość: {order.Quantity}");
            Console.WriteLine($"Cena jednostkowa: {order.Price}");
            Console.WriteLine($"Cena całkowita: {order.TotalPrice}");
        }
        else
        {
            Console.WriteLine($"Nie znaleziono zamówienia o numerze {orderId}.");
        }
    }

    // Metoda wyświetlająca listę wszystkich zamówień
    public void ShowAllOrders()
    {
        Console.WriteLine("Lista wszystkich zamówień:");
        foreach (Order order in orders)
        {
            Console.WriteLine($"Numer zamówienia: {order.OrderId}, Produkt: {order.ProductName}, Ilość: {order.Quantity}, Cena całkowita: {order.TotalPrice}");
        }
    }
}

// Klasa testująca
public class TestOrderSystem
{
    public static void Main()
    {
        OrderSystem orderSystem = new OrderSystem();

        bool quit = false;
        while (!quit)
        {
            Console.WriteLine("\nCo chcesz zrobić?");
            Console.WriteLine("1. Dodaj zamówienie");
            Console.WriteLine("2. Usuń zamówienie");
            Console.WriteLine("3. Wyświetl szczegóły zamówienia");
            Console.WriteLine("4. Wyświetl listę wszystkich zamówień");
            Console.WriteLine("5. Zakończ program");

            string input = Console.ReadLine();
            int choice;
            if (int.TryParse(input, out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Podaj dane nowego zamówienia:");
                        Console.Write("Numer zamówienia: ");
                        int orderId;
                        if (int.TryParse(Console.ReadLine(), out orderId))
                        {
                            Console.Write("Nazwa produktu: ");
                            string productName = Console.ReadLine();
                            Console.Write("Ilość: ");
                            int quantity;
                            if (int.TryParse(Console.ReadLine(), out quantity))
                            {
                                Console.Write("Cena jednostkowa: ");
                                decimal price;
                                if (decimal.TryParse(Console.ReadLine(), out price))
                                {
                                    Order order = new Order { OrderId = orderId, ProductName = productName, Quantity = quantity, Price = price };
                                    orderSystem.AddOrder(order);
                                }
                                else
                                {
                                    Console.WriteLine("Nieprawidłowa cena.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidłowa ilość.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy numer zamówienia.");
                        }
                        break;
                    case 2:
                        Console.Write("Podaj numer zamówienia do usunięcia: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            orderSystem.RemoveOrder(id);
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy numer zamówienia.");
                        }
                        break;
                    case 3:
                        Console.Write("Podaj numer zamówienia do wyświetlenia: ");
                        if (int.TryParse(Console.ReadLine(), out int orderId2))
                        {
                            orderSystem.ShowOrderDetails(orderId2);
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy numer zamówienia.");
                        }
                        break;
                    case 4:
                        orderSystem.ShowAllOrders();
                        break;
                    case 5:
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }
    }
}