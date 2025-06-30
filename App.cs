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
                Console.Write("Enter command: ");
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

                    case "update":
                        Update();
                        break;

                    case "delete":
                        Delete();
                        break;

                    case "sum-all":
                        SumAll();
                        break;

                    case "sum-month":
                        SumMonth();
                        break;

                    case "show-category":
                        ShowCategory();
                        break;

                    case "clear":
                        Clear();
                        break;

                    case "exit":
                        isRunning = false;
                        break;

                    case "help":
                        ShowHelp();
                        break;

                    default:
                        Console.WriteLine("Unknown command. Try 'help' or 'exit'.");
                        break;
                }
            }
            void Add()
            {
                if (!IsUserInputValid(commands, 4))
                    return;

                if (!double.TryParse(commands[2], out double amount))
                {
                    Utility.Utility.PrintErrorMessage("Invalid input. Not a valid amount.");
                    return;
                }

                var expenseId = expenseService.Add(commands[1], amount, commands[3]);

                if (expenseId == 0)
                {
                    Utility.Utility.PrintErrorMessage("Expense adding failed for some reason - category can be misspelled! Please try again...");
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

                var expenses = expenseService.ListAllExpenses();

                foreach (var element in expenses)
                {
                    Console.WriteLine($"Expense id : {element.Id}, expense description : {element.Description}, expense amount : {element.Amount}, expense category : {element.Category}");
                }
            }

            void Update()
            {
                if (!IsUserInputValid(commands, 4))
                    return;

                if (!int.TryParse(commands[2], out int id))
                {
                    Utility.Utility.PrintErrorMessage("Invalid input. Not a valid id.");
                    return;
                }

                if (!double.TryParse(commands[3], out double amount))
                {
                    Utility.Utility.PrintErrorMessage("Invalid input. Not a valid amount.");
                    return;
                }

                var updatedExpenseId = expenseService.UpdateExpense(commands[1], id, amount);

                if (updatedExpenseId == 0)
                {
                    Utility.Utility.PrintErrorMessage("Expense updating failed! Please try again...");
                }
                else
                {
                    Utility.Utility.PrintInfoMessage($"Expense updated successfully (ID : {updatedExpenseId})");
                }
            }

            void Delete()
            {
                if (!IsUserInputValid(commands, 2))
                    return;

                if (!int.TryParse(commands[1], out int id))
                {
                    Utility.Utility.PrintErrorMessage("Invalid input. Not a valid id.");
                    return;
                }

                var deletedExpenseId = expenseService.DeleteExpense(id);

                if (deletedExpenseId == 0)
                {
                    Utility.Utility.PrintErrorMessage("Expense deletion failed! Please try again...");
                }
                else
                {
                    Utility.Utility.PrintInfoMessage($"Expense deleted successfully (ID : {deletedExpenseId})");
                }
            }

            void SumAll()
            {
                if (!IsUserInputValid(commands, 1))
                    return;

                var sumOfExpenses = expenseService.SumAll();

                Utility.Utility.PrintInfoMessage($"Sum of all expenses : {sumOfExpenses}");
            }

            void SumMonth()
            {
                if (!IsUserInputValid(commands, 2))
                    return;

                int.TryParse(commands[1], out int month);

                var result = expenseService.SumMonth(month);
                var monthName = new DateTime(1, month, 1).ToString("MMMM");

                Utility.Utility.PrintInfoMessage(result != 0
                    ? $"Total expenses for {monthName} : ${result}"
                    : "Total expenses: $0");
            }

            void ShowHelp()
            {
                var helpInfo = expenseService.GetHelpCommandsList();
                int count = 1;
                if (helpInfo != null && helpInfo.Count > 0)
                {
                    foreach (var item in helpInfo)
                    {
                        Utility.Utility.PrintHelpMessage(item, count);
                        count++;
                    }
                }

                Console.WriteLine();
            }

            void ShowCategory()
            {
                if (!IsUserInputValid(commands, 2))
                    return;

                var category = expenseService.ShowCategory(commands[1]);
            }

            void Clear() => Console.Clear();
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

            if (requiredParams == 4)
            {
                if (commands.Count != requiredParams || string.IsNullOrEmpty(commands[1]) || string.IsNullOrEmpty(commands[2]) || string.IsNullOrEmpty(commands[3]))
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