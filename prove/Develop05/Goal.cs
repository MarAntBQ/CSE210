using System;

abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }
    public int Score { get; set; }

    public abstract void RecordEvent();
}
