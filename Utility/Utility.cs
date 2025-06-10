using System.Text.RegularExpressions;

namespace ExpenseTracker.Utility
{
    public static class Utility
    {
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
