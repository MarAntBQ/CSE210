class EternalGoal : Goal
{
    public int RepeatedTimes { get; set; }

    public EternalGoal(string name, string description, int points, bool isCompleted, int score, int repeatedTimes)
    {
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = isCompleted;
        Score = score;
        RepeatedTimes = repeatedTimes;
    }

    public override void RecordEvent()
    {
        Score += Points;
        RepeatedTimes++;
        IsCompleted = true;
    }
}