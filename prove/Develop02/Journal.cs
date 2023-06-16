using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<JournalEntry> entries;

    public Journal()
    {
        entries = new List<JournalEntry>();
    }

    public void AddEntry(JournalEntry entry)
    {
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine("Prompt: " + entry.Prompt);
            Console.WriteLine("Response: " + entry.Response);
            Console.WriteLine("Date: " + entry.Date);
            Console.WriteLine();
        }
    }

    public void SaveJournalToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine(entry.Prompt);
                    writer.WriteLine(entry.Response);
                    writer.WriteLine(entry.Date);
                    writer.WriteLine();
                }
            }
            Console.WriteLine("Journal saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while saving the journal: " + ex.Message);
        }
    }

    public void LoadJournalFromFile(string filename)
    {
        try
        {
            entries.Clear();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string prompt = reader.ReadLine();
                    string response = reader.ReadLine();
                    DateTime date = DateTime.Parse(reader.ReadLine());

                    JournalEntry entry = new JournalEntry
                    {
                        Prompt = prompt,
                        Response = response,
                        Date = date
                    };

                    entries.Add(entry);

                    reader.ReadLine(); 
                }
            }
            Console.WriteLine("Journal loaded successfully from file: " + filename);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while loading the journal: " + ex.Message);
        }
    }
}