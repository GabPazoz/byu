using System;


namespace MindfulnessProgram
{
public class BreathingActivity : Activity
{
public BreathingActivity()
{
Name = "Breathing Activity";
Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
}


protected override void RunActivity()
{
Console.WriteLine("Start breathing...\n");



var endTime = DateTime.Now.AddSeconds(DurationSeconds);


while (DateTime.Now < endTime)
{
Console.Write("Breathe in...");
var inSec = Math.Min(4, (int)(endTime - DateTime.Now).TotalSeconds);
if (inSec > 0) ShowCountdown(inSec);


if (DateTime.Now >= endTime) break;


Console.Write("Breathe out...");
var outSec = Math.Min(6, (int)(endTime - DateTime.Now).TotalSeconds);
if (outSec > 0) ShowCountdown(outSec);
}
}
}
}