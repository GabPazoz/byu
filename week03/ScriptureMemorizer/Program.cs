using System;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var reference = new Reference("Proverbs", 3, 5, 6);
            var text = "Trust in the LORD with all your heart and lean not on your own understanding; " +
                       "in all your ways submit to him, and he will make your paths straight.";

            var scripture = new Scripture(reference, text);
            var rng = new Random();

            const int hideCountPerStep = 3; 

            while (true)
            {
                Console.Clear();
                Console.WriteLine(reference.ToString());
                Console.WriteLine();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine();

                if (scripture.AllHidden())
                {
                    Console.WriteLine("All words are hidden. Good job!");
                    break;
                }

                Console.WriteLine("Press Enter to hide a few words, or type 'quit' to exit.");
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && input.Trim().ToLower() == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                scripture.HideRandomWords(hideCountPerStep, rng);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}