class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points, bool isCompleted, int score)
    {
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = isCompleted;
        Score = score;
    }

    public override void RecordEvent()
    {
        IsCompleted = true;
        Score += Points;
    }
}
