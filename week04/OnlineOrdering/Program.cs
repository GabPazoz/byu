class Program
{
    static void Main(string[] args)
    {
        // ===== FIRST ORDER =====
        Address address1 = new Address("123 Main St", "Boston", "MA", "USA");
        Customer customer1 = new Customer("John Smith", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Keyboard", "KB100", 49.99, 1));
        order1.AddProduct(new Product("Mouse", "MS200", 19.99, 2));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():0.00}");
        Console.WriteLine("\n--------------------------\n");

        // ===== SECOND ORDER =====
        Address address2 = new Address("Av. Paulista 500", "São Paulo", "SP", "Brazil");
        Customer customer2 = new Customer("Maria Oliveira", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Monitor", "MN300", 899.99, 1));
        order2.AddProduct(new Product("USB Cable", "CB150", 9.99, 3));

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():0.00}");
    }
}