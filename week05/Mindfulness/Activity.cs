using System;
{

}
}



protected void ShowSpinner(int seconds)
{
var spinner = new[] { "|", "/", "-", "\\" };
int idx = 0;
var end = DateTime.Now.AddSeconds(seconds);
while (DateTime.Now < end)
{
Console.Write(spinner[idx % spinner.Length]);
Thread.Sleep(250);
Console.Write("\b \b");
idx++;
}
Console.WriteLine();
}



protected void ShowCountdown(int seconds)
{
for (int i = seconds; i >= 1; i--)
{
Console.Write(i);
Thread.Sleep(1000);
Console.Write("\b \b");
}
Console.WriteLine();
}



protected void ShowTimedSpinner(int seconds, int sleepMs = 400)
{
var spinner = new[] { "|", "/", "-", "\\" };
int idx = 0;
var end = DateTime.Now.AddSeconds(seconds);
while (DateTime.Now < end)
{
Console.Write(spinner[idx % spinner.Length]);
Thread.Sleep(sleepMs);
Console.Write("\b \b");
idx++;
}
Console.WriteLine();
}



protected abstract void RunActivity();
}
}