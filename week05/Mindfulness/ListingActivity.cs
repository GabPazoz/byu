using System;
using System.Collections.Generic;
using System.Threading;


namespace MindfulnessProgram
{
public class ListingActivity : Activity
{
private readonly List<string> _prompts = new List<string>
{
"Who are people that you appreciate?",
"What are personal strengths of yours?",
"Who are people that you have helped this week?",
"When have you felt the Holy Ghost this month?",
"Who are some of your personal heroes?"
};


public ListingActivity()
{
Name = "Listing Activity";
Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
}


protected override void RunActivity()
{
var rand = new Random();
var prompt = _prompts[rand.Next(_prompts.Count)];


Console.WriteLine("Prompt:\n" + prompt + "\n");
Console.WriteLine("You will have a few seconds to think, then start listing items. Press Enter to continue.");
Console.ReadLine();


Console.WriteLine("Get ready...");
ShowCountdown(5);
Console.WriteLine("Start listing! (press Enter after each item)");


var items = new List<string>();
var endTime = DateTime.Now.AddSeconds(DurationSeconds);


// Read lines until time expires. Uses Console.KeyAvailable to avoid blocking beyond the endTime.
while (DateTime.Now < endTime)
{
if (Console.KeyAvailable)
{
var line = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(line)) items.Add(line.Trim());
}
else
{
Thread.Sleep(100); // small sleep to avoid busy loop
}
}


Console.WriteLine($"\nYou listed {items.Count} items:");
foreach (var it in items)
{
Console.WriteLine("- " + it);
}
}
}
}