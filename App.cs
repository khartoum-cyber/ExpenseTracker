
using System.Text.RegularExpressions;

namespace ExpenseTracker
{
    internal class App
    {
        public void Run()
        {
            var isRunning = true;
            var expenseList = new List<string>();
            var commands = new List<string>();

            while (isRunning)
            {
                Console.Write("Enter command: ");
                var input = Console.ReadLine() ?? string.Empty;
                commands = InputParser(input);
                var command = commands.FirstOrDefault();

                switch (command)
                {
                    case "add":
                        Add();
                        break;

                    case "exit":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Unknown command. Try 'add' or 'exit'.");
                        break;
                }
            }

            return;

            void Add()
            {
                Console.Write("Enter expense to add: ");
                var expense = Console.ReadLine() ?? string.Empty;

                expenseList.Add(expense);

                Console.Write("Your expenses :");
                foreach (var element in expenseList)
                {
                    Console.Write(element);
                }

                Console.WriteLine();
            }
        }

        private static List<string> InputParser(string input)
        {
            var commandArgs = new List<string>();

            var matches = Regex.Matches(input, @"(?<match>[\""'])(?<value>.*?)(?<!\\)\k<match>|(?<value>[^ ]+)");

            foreach (Match match in matches)
            {
                // Remove surrounding quotes if any
                var value = match.Value.Trim('"');
                commandArgs.Add(value);
            }

            return commandArgs;
        }
    }
}
