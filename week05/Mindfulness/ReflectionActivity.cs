using System;
"What made this time different than other times when you were not as successful?",
"What is your favorite thing about this experience?",
"What could you learn from this experience that applies to other situations?",
"What did you learn about yourself through this experience?",
"How can you keep this experience in mind in the future?"
};



private Queue<string> _shuffledQuestions;


public ReflectionActivity()
{
Name = "Reflection Activity";
Description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
_shuffledQuestions = new Queue<string>(Shuffle(new List<string>(_questions)));
}


protected override void RunActivity()
{
var rand = new Random();
var prompt = _prompts[rand.Next(_prompts.Count)];
Console.WriteLine("Prompt:\n" + prompt + "\n");
Console.WriteLine("When you are ready, press Enter to begin reflecting on the following questions.");
Console.ReadLine();


var endTime = DateTime.Now.AddSeconds(DurationSeconds);
while (DateTime.Now < endTime)
{
if (_shuffledQuestions.Count == 0)
{
_shuffledQuestions = new Queue<string>(Shuffle(new List<string>(_questions)));
}


var question = _shuffledQuestions.Dequeue();
Console.WriteLine(question);


var pause = Math.Min(10, (int)(endTime - DateTime.Now).TotalSeconds);
if (pause > 0) ShowTimedSpinner(pause);
}
}


private static List<T> Shuffle<T>(List<T> list)
{
var rand = new Random();
for (int i = list.Count - 1; i > 0; i--)
{
int j = rand.Next(i + 1);
var tmp = list[i];
list[i] = list[j];
list[j] = tmp;
}
return list;
}
}
}