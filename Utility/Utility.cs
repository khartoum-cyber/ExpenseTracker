using System.Text.RegularExpressions;
using ExpenseTracker.Model;

namespace ExpenseTracker.Utility
{
    public static class Utility
    {
        public static void PrintInfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }
        public static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("ERROR:");
            Console.WriteLine(message);
            Console.WriteLine("===================================");
            Console.WriteLine();

            Console.ResetColor();
        }

        public static void PrintHelpMessage(string item, int count)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + count + ". ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(item);
            Console.ResetColor();
        }

        public static void CreateExpenseTable(List<Expense> expenses)
        {
            // Print table header
            Console.WriteLine("{0,-10} | {1,-30} | {2,10} | {3,-15}", "ID", "Description", "Amount", "Category");
            Console.WriteLine(new string('-', 75));

            // Print each expense row
            foreach (var element in expenses)
            {
                Console.WriteLine("{0,-10} | {1,-30} | {2,10:C} | {3,-15}",
                    element.Id,
                    element.Description,
                    element.Amount,
                    element.Category);
            }
        }

        public static List<string> InputParser(string input)
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
