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
                goals.Add(new EternalGoal(name, description, points, false, 0));
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
        List<string> lines = new List<string>();
        foreach (Goal goal in goals)
        {
            lines.Add(goal.Serialize());
        }
        System.IO.File.WriteAllLines(filePath, lines);
        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            goals.Clear();
            foreach (string line in lines)
            {
                Goal goal = Goal.Deserialize(line);
                if (goal !=null)
                {
                    goals.Add(goal);
                }
            }
            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}
