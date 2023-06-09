using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What's your grade percentage? ");
        string answer = Console.ReadLine();
        int percent = int.Parse(answer);

        string letter = "";
        bool passed = true;

        if (percent >= 90) {
            letter = "A";
        } else if (percent >= 80) {
            letter = "B";
        } else if (percent >= 70) {
            letter = "C";
        } else if (percent >= 60) {
            letter = "D";
            passed = false;
        } else {
            letter = "F";
            passed = false;
        }

        // Print Result
        Console.WriteLine($"Your grade is: {letter}");

        if (passed) {
            Console.WriteLine("You passed!");
        } else {
            Console.WriteLine("Better luck next time!");
        }
    }
}