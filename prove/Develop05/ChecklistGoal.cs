class ChecklistGoal : Goal
{
    public int TargetCompletions { get; set; }
    public int BonusPoints { get; set; }
    public int CompletedCompletions { get; set; }

    public ChecklistGoal(string name, string description, int points, bool isCompleted, int targetCompletions, int bonusPoints)
    {
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = isCompleted;
        TargetCompletions = targetCompletions;
        BonusPoints = bonusPoints;
        CompletedCompletions = 0;
    }

    public override void RecordEvent()
    {
        CompletedCompletions++;
        Score += Points;

        if (CompletedCompletions >= TargetCompletions)
        {
            IsCompleted = true;
            Score += BonusPoints; // Update the score with bonus points
        }
    }


}
