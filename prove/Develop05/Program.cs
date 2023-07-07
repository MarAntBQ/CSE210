using System;

class Program
{
    static void Main()
    {
        GoalManager goalManager = new GoalManager();
        string filePath = "goals.txt";

        bool running = true;
        while (running)
        {
            Console.WriteLine($"You have {goalManager.GetTotalScore()} points.");
            Console.WriteLine("Menu options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.WriteLine();

            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    goalManager.CreateGoal();
                    break;
                case "2":
                    goalManager.ListGoals();
                    break;
                case "3":
                    goalManager.SaveGoals(filePath);
                    break;
                case "4":
                    goalManager.LoadGoals(filePath);
                    break;
                case "5":
                    goalManager.RecordEvent();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
