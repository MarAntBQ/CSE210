using System;
using System.Threading;

public abstract class Activity
{
    protected int duration;

    protected Activity(int duration)
    {
        this.duration = duration;
    }

    public abstract void Start();
    public abstract void End();

    protected void DisplayWelcomeMessage(string activityName, string activityPurpose)
    {
        Console.Clear();
        Console.WriteLine($"{activityName} Activity");
        Console.WriteLine(new string('-', activityName.Length + 10));
        Console.WriteLine(activityPurpose);
        Console.WriteLine();
    }
    protected void Spinner(int totalTime)
    {
        for (int j = totalTime; j >= 0; j--)
        {
            Console.Write("+");
            Thread.Sleep(1000);
            Console.Write("\b \b"); // Erase the + character
            Console.Write("-"); // Replace it with the - character
        }
        Console.WriteLine();
    }
    protected void Counter(int totalTime)
    {
        for (int j = totalTime; j >= 0; j--)
        {
            Console.Write($"{j} ");          
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}