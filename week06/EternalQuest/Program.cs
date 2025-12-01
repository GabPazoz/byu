// Eternal Quest - Week 06
// Author:Gabriel Luiz Batista Passo
// Comments explaining extra features in simple English:
// - Level system added: your level increases when your total score passes certain thresholds.
// - Badge system: you earn badges automatically after reaching score milestones.
// - Optional "negative goals" idea described in comments for future expansion.
// - Simple save/load using text lines for easy debugging.
// - Factory-like reconstruction logic added inside loading function.
//---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

// ===============================
// BASE CLASS: Goal
// ===============================
// This class represents the common structure for all types of goals.
// It uses protected fields so derived classes can access them.
abstract class Goal
{
    protected string _title;          // Title of the goal
    protected string _description;    // Description of the goal
    protected int _points;            // Points given when the goal makes progress
    protected bool _isComplete;       // Tracks if the goal is completed (some types never complete)

    public string Title => _title;    // Read-only properties
    public string Description => _description;
    public int Points => _points;
    public bool IsComplete => _isComplete;

    // Constructor called by all derived classes
    protected Goal(string title, string description, int points)
    {
        _title = title;
        _description = description;
        _points = points;
        _isComplete = false; // Default
    }

    // Each type of goal must define how an event is recorded.
    public abstract int RecordEvent();

    // Returns a line of text to save the goal into a file.
    public virtual string GetStringRepresentation()
    {
        return $"{GetType().Name}|{Escape(_title)}|{Escape(_description)}|{_points}|{_isComplete}";
    }

    // Simple default display: shows if complete and the description.
    public virtual string GetDetailsString()
    {
        return $"[{(_isComplete ? "X" : " ")}] {_title} ({_description})";
    }

    // Helper methods to safely store text with '|'
    protected static string Escape(string s) => s?.Replace("|", "<PIPE>") ?? "";
    protected static string Unescape(string s) => s?.Replace("<PIPE>", "|") ?? "";
}

// ===============================
// SIMPLEGOAL: completes once
// ===============================
// This goal gives points one time and then becomes complete.
class SimpleGoal : Goal
{
    public SimpleGoal(string title, string desc, int points) : base(title, desc, points) { }

    public override int RecordEvent()
    {
        // If already complete, nothing happens.
        if (_isComplete) return 0;

        _isComplete = true;  // Marks the goal as complete
        return _points;      // Award points only once
    }
}

// ===============================
// ETERNALGOAL: never completes
// ===============================
// This goal never becomes "complete" and gives points every time it is recorded.
class EternalGoal : Goal
{
    public EternalGoal(string title, string desc, int points) : base(title, desc, points)
    {
        // Eternal goal stays incomplete forever.
    }

    public override int RecordEvent()
    {
        // Always give the same amount of points.
        return _points;
    }

    public override string GetDetailsString()
    {
        // Displays that this is an eternal goal.
        return $"[ ] {_title} (Eternal) - each record: {_points} pts";
    }
}

// ===============================
// CHECKLISTGOAL: needs N completions
// ===============================
// This goal gives points every time, but gives a bonus when fully completed.
class ChecklistGoal : Goal
{
    private int _targetCount;  // How many times needed to finish
    private int _currentCount; // How many times done so far
    private int _bonusPoints;  // Extra points when finished

    public ChecklistGoal(string title, string desc, int pointsPer, int targetCount, int bonusPoints)
        : base(title, desc, pointsPer)
    {
        _targetCount = targetCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (_isComplete) return 0; // Already finished

        _currentCount++;           // One more step completed
        int awarded = _points;     // Base points

        if (_currentCount >= _targetCount)
        {
            _isComplete = true;    // Mark complete
            awarded += _bonusPoints; // Add final bonus
        }

        return awarded;
    }

    public override string GetStringRepresentation()
    {
        return $"{GetType().Name}|{Escape(_title)}|{Escape(_description)}|{_points}|{_isComplete}|{_targetCount}|{_currentCount}|{_bonusPoints}";
    }

    public override string GetDetailsString()
    {
        return $"[{(_isComplete ? "X" : " ")}] {_title} ({_description}) - Completed {_currentCount}/{_targetCount}";
    }

    // Rebuilds a ChecklistGoal from saved values
    public static ChecklistGoal CreateFromParts(string title, string desc, int points, bool isComplete, int target, int current, int bonus)
    {
        var g = new ChecklistGoal(title, desc, points, target, bonus);
        g._currentCount = current;
        g._isComplete = isComplete;
        return g;
    }
}

// ===============================
// GOAL MANAGER
// ===============================
// This class manages the list of goals, score, saving, and loading.
class GoalManager
{
    private List<Goal> _goals = new List<Goal>(); // Stores all goals
    private int _score = 0;                       // Player score

    // Simple level system based on score.
    private int GetLevel()
    {
        if (_score < 1000) return 1;
        if (_score < 2500) return 2;
        if (_score < 5000) return 3;
        if (_score < 10000) return 4;
        return 5;
    }

    public void AddGoal(Goal g) => _goals.Add(g);

    public void ShowGoals()
    {
        Console.WriteLine("Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void ShowScoreAndStatus()
    {
        Console.WriteLine($"Score: {_score} pts | Level: {GetLevel()}");
        ShowBadges();
    }

    // Shows earned badges according to score.
    private void ShowBadges()
    {
        List<string> badges = new List<string>();
        if (_score >= 1000) badges.Add("Bronze Achiever");
        if (_score >= 5000) badges.Add("Silver Achiever");
        if (_score >= 10000) badges.Add("Gold Achiever");

        if (badges.Count == 0)
            Console.WriteLine("Badges: None yet. Keep going!");
        else
            Console.WriteLine($"Badges: {string.Join(", ", badges)}");
    }

    // Records progress for a chosen goal.
    public void RecordEventForGoalIndex(int index)
    {
        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("Invalid index.");
            return;
        }

        var goal = _goals[index];
        int awarded = goal.RecordEvent();

        if (awarded > 0)
        {
            _score += awarded; // Add points to player score
            Console.WriteLine($"Event recorded! You earned {awarded} points.");
        }
        else
        {
            Console.WriteLine("Nothing happened (maybe this goal is already complete).");
        }
    }

    // Saves score and all goals into a file.
    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score); // First line: score
            foreach (var g in _goals)
            {
                writer.WriteLine(g.GetStringRepresentation());
            }
        }
        Console.WriteLine($"Saved to {filename}");
    }

    // Loads score and goals from a file.
    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAll
