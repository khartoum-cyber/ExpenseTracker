using System.Text.Json;
using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Services
{
    internal class ExpenseService : IExpenseService
    {
        private static string fileName = "myExpenses.json";
        private static string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        public int Add(string description, double amount, string category)
        {
            try
            {
                List<Expense> expenseList = new();

                Expense expense = new Expense()
                {
                    Amount = amount,
                    CreatedAt = DateTime.Now,
                    Description = description,
                    Id = GetId(),
                    Category = Enum.TryParse(category, out Categories cat) ? cat : throw new Exception("Category not found")
                };

                var fileExists = CheckAndCreateFile();

                if (fileExists)
                {
                    expenseList = GetAllExpensesFromFile();

                    expenseList?.Add(expense);
                    var updatedExpenseList = JsonSerializer.Serialize<List<Expense>>(expenseList);
                    File.WriteAllText(filePath, updatedExpenseList);

                    return expense.Id;
                }

                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<Expense> ListAllExpenses()
        {
            if (!CheckIfFileExists())
            {
                Utility.Utility.PrintErrorMessage("No expense file found.");
                return new List<Expense>();
            }

            var expensesFromFile = GetAllExpensesFromFile();

            if (expensesFromFile.Count == 0)
            {
                Utility.Utility.PrintErrorMessage("Expense file is empty.");
                return new List<Expense>();
            }

            return expensesFromFile;
        }

        public int UpdateExpense(string description, int id, double amount)
        {
            if (!CheckIfFileExists())
            {
                Utility.Utility.PrintErrorMessage("No expenses to update.");
                return 0;
            }

            var expensesFromFile = GetAllExpensesFromFile();

            if (expensesFromFile.Count > 0)
            {
                var expenseToBeUpdated = expensesFromFile.SingleOrDefault(x => x.Id == id);

                if (expenseToBeUpdated != null)
                {
                    var updatedExpense = new Expense { Description = description, Id = id , Amount = amount, Category = expenseToBeUpdated.Category};

                    expensesFromFile.Remove(expenseToBeUpdated);
                    expensesFromFile.Add(updatedExpense);

                    var updatedExpenseList = JsonSerializer.Serialize(expensesFromFile);
                    File.WriteAllText(filePath, updatedExpenseList);

                    return updatedExpense.Id;
                }
            }

            return 0;
        }

        public int DeleteExpense(int id)
        {
            if (!CheckIfFileExists())
            {
                Utility.Utility.PrintErrorMessage("No expenses to delete.");
                return 0;
            }

            var expensesFromFile = GetAllExpensesFromFile();

            if (expensesFromFile.Count > 0)
            {
                var expenseToBeRemoved = expensesFromFile.SingleOrDefault(x => x.Id == id);

                if (expenseToBeRemoved != null)
                {
                    expensesFromFile.Remove(expenseToBeRemoved);

                    var updatedExpenseList = JsonSerializer.Serialize(expensesFromFile);
                    File.WriteAllText(filePath, updatedExpenseList);

                    return expenseToBeRemoved.Id;
                }
            }

            return 0;
        }
        public double SumAll()
        {
            if (!CheckIfFileExists())
            {
                Utility.Utility.PrintErrorMessage("No expense file found.");
                return 0;
            }

            var expensesFromFile = GetAllExpensesFromFile();

            var sumOfExpenses = expensesFromFile.Select(e => e.Amount).Sum();

            return sumOfExpenses;
        }

        public double SumMonth(int month)
        {
            if (!CheckIfFileExists())
                return 0;

            var expensesFromFile = GetAllExpensesFromFile();

            if (expensesFromFile.Count > 0)
            {
                var sum = expensesFromFile.Where(e => e.CreatedAt.Month == month).Sum(x => x.Amount);
                return sum;
            }

            return 0;
        }

        public List<Expense> ShowCategory(string category)
        {
            throw new NotImplementedException();
        }

        public List<string> GetHelpCommandsList()
        {
            return new List<string>
            {
                "add \"description\" $amount \"CategoryName\"",
                "list",
                "sum-all",
                "sum-month monthNumber",
                "delete id",
                "clear",
                "exit",
                "help",
                "available categories - \"Hardware\" \"Education\" \"Savings\" \"Investments\" \"General\""
            };
        }

        private static List<Expense> GetAllExpensesFromFile()
        {
            List<Expense> fileExpenses = new List<Expense>();
            var jsonData = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(jsonData))
                fileExpenses = JsonSerializer.Deserialize<List<Expense>>(jsonData);

            return fileExpenses ?? new List<Expense>();
        }

        private static bool CheckAndCreateFile()
        {
            try
            {
                var fileExists = CheckIfFileExists();

                if (!fileExists)
                {
                    using FileStream fs = File.Create(filePath);
                    Console.WriteLine($"File {fileName} created successfully.");
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static bool CheckIfFileExists() => File.Exists(filePath);

        private int GetId()
        {
            if (!CheckIfFileExists())
                return 1;

            var expensesFromFile = GetAllExpensesFromFile();

            if (expensesFromFile?.Count > 0)
            {
                expensesFromFile.OrderBy(x => x.Id);
                var lastId = expensesFromFile.Last().Id;
                return lastId + 1;
            }

            return 0;
        }

    }
}
