using System;
using System.Collections.Generic;


namespace MindfulnessProgram
{
class Program
{
static void Main(string[] args)
{
Console.OutputEncoding = System.Text.Encoding.UTF8;
var activities = new List<Activity>
{
new BreathingActivity(),
new ReflectionActivity(),
new ListingActivity()
};


while (true)
{
Console.Clear();
Console.WriteLine("=== Mindfulness Program ===\n");
Console.WriteLine("Choose an activity:");
for (int i = 0; i < activities.Count; i++)
{
Console.WriteLine($"{i+1}. {activities[i].GetType().Name.Replace("Activity", " Activity")}");
}
Console.WriteLine("4. Exit");
Console.Write("Select an option: ");
var input = Console.ReadLine()?.Trim();


if (input == "4" || string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
{
Console.WriteLine("Goodbye. Take care!");
break;
}


if (!int.TryParse(input, out int choice) || choice < 1 || choice > activities.Count)
{
Console.WriteLine("Invalid selection. Press Enter to try again.");
Console.ReadLine();
continue;
}


var activity = activities[choice - 1];
activity.Start();


Console.WriteLine("Press Enter to return to the menu...");
Console.ReadLine();
}
}
}
}