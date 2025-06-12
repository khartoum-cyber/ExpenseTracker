

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

                commands = Utility.Utility.InputParser(input);
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
            void Add()
            {
                if (!IsUserInputValid(commands, 2))
                    return;
            }
        }

        private bool IsUserInputValid(List<string> commands, int requiredParams)
        {
            bool validInput = true;

            if (requiredParams == 1)
            {
                if (commands.Count != requiredParams)
                {
                    validInput = false;
                }
            }

            if (requiredParams == 2)
            {
                if (commands.Count != requiredParams || string.IsNullOrEmpty(commands[1]))
                {
                    validInput = false;
                }
            }

            if (requiredParams == 3)
            {
                if (commands.Count != requiredParams || string.IsNullOrEmpty(commands[1]) || string.IsNullOrEmpty(commands[2]))
                {
                    validInput = false;
                }
            }

            if (!validInput)
            {

                Helper.PrintErrorMessage("Wrong command! Try again.");
                Helper.PrintInfoMessage("Type \"help\" to know the set of commands");
                return false;
            }

            return true;
        }
    }
}
