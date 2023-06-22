using System;

class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture("Moroni 10:3", "Behold, I would exhort you that when ye shall read these things, if it be wisdom in God that ye should read them, that ye would remember how merciful the Lord hath been unto the children of men, from the creation of Adam even down until the time that ye shall receive these things, and ponder it in your hearts.");

        while (!scripture.AllWordsHidden)
        {
            Console.Clear();
            scripture.Display();

            Console.WriteLine();
            Console.WriteLine("\nPress enter to continue or type 'quit' to finish:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
        }
    }
}