using System;

class Program
{
    static int totalActivityCount = 0;
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("DailyHelp App");
            Console.WriteLine("------------------------");
            Console.WriteLine("Make your day better!");
            Console.WriteLine("------------------------");
            Console.WriteLine("SELECT ONE OF OUR ACTIVITIES:");
            Console.WriteLine();
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.WriteLine();

            Console.Write("Enter your choice (1-4): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Breathing Activity Selected");
                    Console.WriteLine("---------------------------");
                    Console.Write("Enter the duration in seconds: ");
                    int breathingDuration = int.Parse(Console.ReadLine());

                    Activity breathingActivity = new BreathingActivity(breathingDuration);
                    breathingActivity.Start();
                    breathingActivity.End();
                    totalActivityCount++;
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Reflection Activity Selected");
                    Console.WriteLine("---------------------------");
                    Console.Write("Enter the duration in seconds: ");
                    int reflectionDuration = int.Parse(Console.ReadLine());

                    Activity reflectionActivity = new ReflectionActivity(reflectionDuration);
                    reflectionActivity.Start();
                    reflectionActivity.End();
                    totalActivityCount++;
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Listing Activity Selected");
                    Console.WriteLine("---------------------------");
                    Console.Write("Enter the duration in seconds: ");
                    int listingDuration = int.Parse(Console.ReadLine());

                    Activity listingActivity = new ListingActivity(listingDuration);
                    listingActivity.Start();
                    listingActivity.End();
                    totalActivityCount++;
                    break;
                case "4":
                    Console.WriteLine($"Total activities performed: {totalActivityCount}");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
