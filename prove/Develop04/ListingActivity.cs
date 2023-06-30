public class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        DisplayWelcomeMessage("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");

        Random random = new Random();

        Console.WriteLine("Prompt:");
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Console.WriteLine();

        Console.WriteLine("Get ready to list items...");
        Counter(3);

        Console.WriteLine();

        List<string> items = new List<string>();

        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("Enter an item: ");
            string item = Console.ReadLine();

            items.Add(item);
        }

        Console.WriteLine();

        Console.WriteLine("Number of items entered: " + items.Count);
        Console.WriteLine();

        Console.WriteLine("Great job! You have completed the listing activity.");
        Console.WriteLine();
        Console.WriteLine($"Duration: {duration} seconds");
        Thread.Sleep(3000);
    }

    public override void End() { }
}