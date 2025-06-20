

using ExpenseTracker.Services;

namespace ExpenseTracker
{
    internal class App(IExpenseService expenseService)
    {
        public void Run()
        {
            var isRunning = true;
            var expenseList = new List<string>();
            var commands = new List<string>();

            while (isRunning)
            {
                Console.WriteLine("Enter command: ");
                var input = Console.ReadLine() ?? string.Empty;

                commands = Utility.Utility.InputParser(input);
                var command = commands.FirstOrDefault();

                switch (command)
                {
                    case "add":
                        Add();
                        break;

                    case "list":
                        ListAll();
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
                if (!IsUserInputValid(commands, 4))
                    return;

                if (!commands[1].Contains("\"") || !commands[3].Contains("\""))
                {
                    Utility.Utility.PrintErrorMessage("Description and/or category must be in double quotes \"your description\" \"your category\"");
                    return;
                }

                var expenseId = expenseService.Add(commands[1], Convert.ToDouble(commands[2]), commands[3]);

                if (expenseId == 0)
                {
                    Utility.Utility.PrintErrorMessage("Expense adding failed for some reason! Please try again...");
                }
                else
                {
                    Utility.Utility.PrintInfoMessage($"Expense added successfully (ID : {expenseId})");
                }
            }

            void ListAll()
            {
                if (!IsUserInputValid(commands, 1))
                    return;

                var expenses = expenseService.GetAllExpenses();

                foreach (var element in expenses)
                {
                    Console.WriteLine($"Expense id : {element.Id}, expense description : {element.Description}, expense amount : {element.Amount}, expense category : {element.Category}");
                }
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

                Utility.Utility.PrintErrorMessage("Wrong command! Try again.");
                Utility.Utility.PrintInfoMessage("Type \"help\" to know the set of commands");
                return false;
            }

            return true;
        }
    }
}
