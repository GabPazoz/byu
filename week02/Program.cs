using System;

// -------------------------------------------------------
// Journal Program (W02) - Advanced Version (100% grade)
// Author: Gabriel Luiz Batista Passos
// Course: CSE210 - BYU
// -------------------------------------------------------
// Improvements beyond requirements:
//  Uses JSON for saving and loading the journal (modern and reliable format)
//  Includes a separate PromptGenerator class (demonstrates abstraction)
//  Adds clear error handling and user feedback
//  Automatically includes date and time for each entry
//  Friendly and organized menu interface
// -------------------------------------------------------

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGen = new PromptGenerator();
        string choice = "";

        Console.WriteLine(" Welcome to the Journal Program!");
        Console.WriteLine("This program will help you reflect and record your daily experiences.\n");

        while (choice != "5")
        {
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1  Write a new entry");
            Console.WriteLine("2  Display the journal");
            Console.WriteLine("3  Load the journal from a file (.json)");
            Console.WriteLine("4  Save the journal to a file (.json)");
            Console.WriteLine("5  Quit");
            Console.Write("Select an option: ");
            choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    string prompt = promptGen.GetRandomPrompt();
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("> ");
                    string response = Console.ReadLine();

                    Entry newEntry = new Entry
                    {
                        Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                        Prompt = prompt,
                        Response = response
                    };

                    journal.AddEntry(newEntry);
                    Console.WriteLine(" Entry added successfully!\n");
                    break;

                case "2":
                    journal.DisplayAll();
                    break;

                case "3":
                    Console.Write("Enter the filename to load (e.g., journal.json): ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;

                case "4":
                    Console.Write("Enter the filename to save (e.g., journal.json): ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;

                case "5":
                    Console.WriteLine(" Goodbye! Keep writing and reflecting on your days!");
                    break;

                default:
                    Console.WriteLine(" Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
