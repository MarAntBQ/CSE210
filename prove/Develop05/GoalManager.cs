using System;
using System.Collections.Generic;

class GoalManager
{
    private List<Goal> goals = new List<Goal>();

    public int GetTotalScore()
    {
        int totalScore = 0;
        foreach (Goal goal in goals)
        {
            if (goal is ChecklistGoal checklistGoal)
            {
                int bonusPoints = (checklistGoal.CompletedCompletions >= checklistGoal.TargetCompletions) ? checklistGoal.BonusPoints : 0;
                totalScore += (checklistGoal.Points * checklistGoal.CompletedCompletions) + bonusPoints;
            }
            else if (goal is EternalGoal eternalGoal)
            {
                int repeatedTimes = eternalGoal.RepeatedTimes;
                totalScore += eternalGoal.Points * repeatedTimes;
            }
            else if (goal.IsCompleted)
            {
                totalScore += goal.Score;
            }
        }
        return totalScore;
    }


    public void CreateGoal()
    {
        Console.WriteLine("Enter the goal type (1 for Simple, 2 for Eternal, 3 for Checklist):");
        string type = Console.ReadLine();
        Console.WriteLine("Enter the goal name:");
        string name = Console.ReadLine();
        Console.WriteLine("Enter the goal description:");
        string description = Console.ReadLine();
        Console.WriteLine("Enter the goal points:");
        int points = Convert.ToInt32(Console.ReadLine());

        switch (type)
        {
            case "1":
                goals.Add(new SimpleGoal(name, description, points, false, 0));
                Console.WriteLine("Simple goal created successfully.");
                break;
            case "2":
                goals.Add(new EternalGoal(name, description, points, false, 0, 0));
                Console.WriteLine("Eternal goal created successfully.");
                break;
            case "3":
                Console.WriteLine("Enter the number of times the goal needs to be accomplished for a bonus:");
                int completions = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the bonus points for accomplishing the goal that many times:");
                int bonusPoints = Convert.ToInt32(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, description, points, false, completions, bonusPoints));
                Console.WriteLine("Checklist goal created successfully.");
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
    }

    public void ListGoals()
    {
        Console.WriteLine("Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Goal goal = goals[i];
            string completionStatus = goal is EternalGoal ? "[ ]" : (goal.IsCompleted ? "[X]" : "[ ]");
            string goalInfo = $"{i + 1}. {completionStatus} {goal.Name} ({goal.Description})";
            if (goal is ChecklistGoal checklistGoal)
            {
                goalInfo += $" -- Currently completed: {checklistGoal.CompletedCompletions}/{checklistGoal.TargetCompletions}";
            }
            Console.WriteLine(goalInfo);
        }
    }


    public void RecordEvent()
    {
        Console.WriteLine("The pending goals are:");
        List<Goal> activeGoals = goals.Where(goal => !(goal is SimpleGoal simpleGoal && simpleGoal.IsCompleted) && !(goal is ChecklistGoal checklistGoal && checklistGoal.IsCompleted)).ToList();
        for (int i = 0; i < activeGoals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {activeGoals[i].Name}");
        }
        if (activeGoals.Count == 0)
        {
            Console.WriteLine("No pending goals.");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Enter the index of the goal you want to record an event for:");
        int index = Convert.ToInt32(Console.ReadLine()) - 1;
        if (index >= 0 && index < activeGoals.Count)
        {
            Goal goal = activeGoals[index];
            goal.RecordEvent();
            Console.WriteLine("Event recorded successfully.");
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
    }

    public void SaveGoals(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(GetTotalScore().ToString());

            foreach (Goal goal in goals)
            {
                if (goal is SimpleGoal simpleGoal)
                {
                    writer.WriteLine($"SimpleGoal:{simpleGoal.Name},{simpleGoal.Description},{simpleGoal.Points},{simpleGoal.IsCompleted}");
                }
                else if (goal is EternalGoal eternalGoal)
                {
                    writer.WriteLine($"EternalGoal:{eternalGoal.Name},{eternalGoal.Description},{eternalGoal.Points},{eternalGoal.RepeatedTimes}");
                }
                else if (goal is ChecklistGoal checklistGoal)
                {
                    writer.WriteLine($"CheckListGoal:{checklistGoal.Name},{checklistGoal.Description},{checklistGoal.Points},{checklistGoal.BonusPoints},{checklistGoal.TargetCompletions},{checklistGoal.CompletedCompletions}");
                }
            }
        }

        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals(string filePath)
    {
        if (File.Exists(filePath))
        {
            goals.Clear();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();

                if (int.TryParse(line, out int score))
                {
                    // Set the total score
                    Console.WriteLine($"Total score set to: {score}");
                }
                else
                {
                    Console.WriteLine("Invalid file format.");
                    return;
                }

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');

                    if (parts.Length == 2)
                    {
                        string goalType = parts[0];
                        string goalData = parts[1];

                        string[] data = goalData.Split(',');

                        switch (goalType)
                        {
                            case "SimpleGoal":
                                if (data.Length == 4)
                                {
                                    string name = data[0];
                                    string description = data[1];
                                    int points = Convert.ToInt32(data[2]);
                                    bool isCompleted = Convert.ToBoolean(data[3]);
                                    goals.Add(new SimpleGoal(name, description, points, isCompleted, 0));
                                }
                                break;
                            case "EternalGoal":
                                if (data.Length == 4)
                                {
                                    string name = data[0];
                                    string description = data[1];
                                    int points = Convert.ToInt32(data[2]);
                                    int repeatedTimes = Convert.ToInt32(data[3]);
                                    goals.Add(new EternalGoal(name, description, points, false, 0, repeatedTimes));
                                }
                                break;
                            case "CheckListGoal":
                                if (data.Length == 6)
                                {
                                    string name = data[0];
                                    string description = data[1];
                                    int points = Convert.ToInt32(data[2]);
                                    int bonusPoints = Convert.ToInt32(data[3]);
                                    int targetCompletions = Convert.ToInt32(data[4]);
                                    int completedCompletions = Convert.ToInt32(data[5]);
                                    bool isCompleted = completedCompletions >= targetCompletions;
                                    ChecklistGoal checklistGoal = new ChecklistGoal(name, description, points, isCompleted, targetCompletions, bonusPoints);
                                checklistGoal.CompletedCompletions = completedCompletions;

                                // Determine if it is currently completing or completed
                                if (completedCompletions < targetCompletions)
                                {
                                    checklistGoal.IsCompleted = false;
                                }
                                else
                                {
                                    checklistGoal.IsCompleted = true;
                                }

                                goals.Add(checklistGoal);
                                }
                                break;
                            default:
                                Console.WriteLine($"Unknown goal type: {goalType}");
                                break;
                        }
                    }
                }
            }

            Console.WriteLine($"Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }

}
