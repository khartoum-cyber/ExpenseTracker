using ExpenseTracker.Model;

namespace ExpenseTracker.Services
{
    internal class ExpenseService : IExpenseService
    {
        private static string fileName = "myExpenses.json";
        private static string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        List<Expense> expenseList = new();
        public int Add(string description, double amount, string category)
        {
            try
            {
                Expense expense = new Expense()
                {
                    Amount = amount,
                    CreatedAt = DateTime.Now,
                    Description = description,
                    Id = GetId(),
                    Category = category
                };

                //expenseList.Add(expense);
                //return expense.Id;

                var fileExists = CheckAndCreateFile();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<Expense> GetAllExpenses()
        {
            return expenseList;
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
            if (expenseList.Count == 0)
                return 1;

            expenseList.OrderBy(x => x.Id);
            var lastId = expenseList.Last().Id;
            return lastId + 1;
        }
    }
}
