using System;

abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }
    public int Score { get; set; }

    public abstract void RecordEvent();

    public string Serialize()
    {
        return $"{GetType().Name},{Name},{Description},{Points},{IsCompleted},{Score}";
    }

    public static Goal Deserialize(string data)
    {
        string[] parts = data.Split(',');
        if (parts.Length == 6)
        {
            string goalType = parts[0];
            string name = parts[1];
            string description = parts[2];
            int points = Convert.ToInt32(parts[3]);
            bool isCompleted = Convert.ToBoolean(parts[4]);
            int score = Convert.ToInt32(parts[5]);
    
            switch (goalType)
            {
                case nameof(SimpleGoal):
                    return new SimpleGoal(name, description, points, isCompleted, score);
                case nameof(EternalGoal):
                    return new EternalGoal(name, description, points, isCompleted, score);
                default:
                    Console.WriteLine($"Unknown goal type: {goalType}");
                    return null;
            }
        }
        else if (parts.Length == 8)
        {
            string goalType = parts[0];
            string name = parts[1];
            string description = parts[2];
            int points = Convert.ToInt32(parts[3]);
            bool isCompleted = Convert.ToBoolean(parts[4]);
            int completions = Convert.ToInt32(parts[5]);
            int bonusPoints = Convert.ToInt32(parts[6]);
    
            switch (goalType)
            {
                case nameof(ChecklistGoal):
                    return new ChecklistGoal(name, description, points, isCompleted, completions, bonusPoints);
                default:
                    Console.WriteLine($"Unknown goal type: {goalType}");
                    return null;
            }
        }
    
        Console.WriteLine($"Invalid goal data: {data}");
        return null;
    }
}
