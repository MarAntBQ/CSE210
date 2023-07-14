using System;

class Program
{
    static void Main(string[] args)
    {
        // Initialize your e-commerce application
        ECommerceApp app = new ECommerceApp();

        // Display the main menu and handle user interactions
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Marco Antonio E-commerce");
            Console.WriteLine("------------------------");
            Console.WriteLine("1. User System");
            Console.WriteLine("2. Admin System");
            Console.WriteLine("3. Exit");
            Console.WriteLine("------------------------");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    app.RunUserSystem();
                    break;
                case "2":
                    app.RunAdminSystem();
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Exit the application
        Console.WriteLine("Thank you for using Marco Antonio E-commerce. Goodbye!");
    }
}