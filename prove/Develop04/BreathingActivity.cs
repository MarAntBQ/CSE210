using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        DisplayWelcomeMessage("Breathing", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.");

        bool breathIn = true;
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.WriteLine();
            if (breathIn) {
              Console.WriteLine("Take a deep breath in...");
              Thread.Sleep(1000);
              breathIn = false;
              //Console.WriteLine("Bool is " + breathIn);
            } else {
              Console.WriteLine("And exhale slowly...");
              Thread.Sleep(1000);
              breathIn = true;
              //Console.WriteLine("Bool is " + breathIn);
            }
            Counter(4);
        }
        Console.WriteLine();
        Console.WriteLine("Great job! You have completed the breathing activity.");
        Console.WriteLine();
        Console.WriteLine($"Duration: {duration} seconds");
        Thread.Sleep(3000);
    }

    public override void End() {}
}
