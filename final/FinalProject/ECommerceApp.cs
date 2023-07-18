using System;

class ECommerceApp
{
    private UserSystem userSystem;
    private ManagementSystem managementSystem;
    private Database database;

    public ECommerceApp()
    {
        database = new Database();
        userSystem = new UserSystem(database);
        managementSystem = new ManagementSystem(database);
    }

    public void RunUserSystem()
    {
        // User System Menu
        bool exitUserSystem = false;
        while (!exitUserSystem)
        {
            Console.Clear();
            Console.WriteLine("Marco Antonio E-commerce - Front Office");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Add Product to Cart");
            Console.WriteLine("3. View Cart");
            Console.WriteLine("4. Checkout");
            Console.WriteLine("5. See My Orders");
            Console.WriteLine("6. Go Back to Main Menu");
            Console.WriteLine("--------------------------------------");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    userSystem.ViewProducts();
                    break;
                case "2":
                    userSystem.AddProductToCart();
                    break;
                case "3":
                    userSystem.ViewCart();
                    break;
                case "4":
                    userSystem.Checkout();
                    break;
                case "5":
                    userSystem.ViewMyOrders();
                    break;
                case "6":
                    exitUserSystem = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (!exitUserSystem)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public void RunAdminSystem()
    {
        // Admin System Menu
        bool exitAdminSystem = false;
        while (!exitAdminSystem)
        {
            Console.Clear();
            Console.WriteLine("Marco Antonio E-commerce - Back Office");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Stock");
            Console.WriteLine("3. Remove Product");
            Console.WriteLine("4. Go Back to Main Menu");
            Console.WriteLine("-------------------------------------");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    managementSystem.AddProductWithUserInput();
                    break;
                case "2":
                    managementSystem.UpdateStock();
                    break;
                case "3":
                    managementSystem.RemoveProduct();
                    break;
                case "4":
                    exitAdminSystem = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (!exitAdminSystem)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}