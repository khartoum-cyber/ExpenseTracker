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

                if (!double.TryParse(commands[2], out double amount))
                {
                    Utility.Utility.PrintErrorMessage("Invalid input. Not a valid amount.");
                    return;
                }

                var expenseId = expenseService.Add(commands[1], amount, commands[3]);

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

                var expenses = expenseService.ListAllExpenses();

                foreach (var element in expenses)
                {
                    Console.WriteLine($"Expense id : {element.Id}, expense description : {element.Description}, expense amount : {element.Amount}, expense category : {element.Category}");
                }
            }

            void Update()
            {
                if (!IsUserInputValid(commands, 2))
                    return;

                if (!int.TryParse(commands[1], out int id))
                {
                    Utility.Utility.PrintErrorMessage("Invalid input. Not a valid id.");
                    return;
                }

                var updatedExpense = expenseService.UpdateExpense(id);
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