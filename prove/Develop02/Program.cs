using System;

public class Program
{
    private static Journal journal = new Journal();
    private static Random random = new Random();

    public static void Main()
    {
        bool exitProgram = false;

        while (!exitProgram)
        {
            Console.WriteLine("MARCO ANTONIO - JOURNAL SYSTEM");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Select an option: ");
            string userSelection = Console.ReadLine();
            Console.WriteLine();

            switch (userSelection)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournalToFile();
                    break;
                case "4":
                    LoadJournalFromFile();
                    break;
                case "5":
                    exitProgram = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine();
                    break;
            }
        }
    }

    private static void WriteNewEntry()
    {
        string[] prompts = GetPrompts();

        string prompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine("Prompt: " + prompt);
        Console.Write("Response: ");
        string response = Console.ReadLine();

        JournalEntry entry = new JournalEntry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now
        };

        journal.AddEntry(entry);

        Console.WriteLine();
        Console.WriteLine("Entry added successfully!");
        Console.WriteLine();
    }

    private static void DisplayJournal()
    {
        journal.DisplayEntries();
    }

    private static void SaveJournalToFile()
    {
        Console.WriteLine("Enter the filename to save the journal:");
        string filename = Console.ReadLine();

        journal.SaveJournalToFile(filename);
        Console.WriteLine();
    }

    private static void LoadJournalFromFile()
    {
        Console.WriteLine("Enter the filename to load the journal:");
        string filename = Console.ReadLine();

        journal.LoadJournalFromFile(filename);
        Console.WriteLine();
    }

    private static string[] GetPrompts()
    {
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What did you learn from your scriptures study today?"
        };

        return prompts;
    }
}