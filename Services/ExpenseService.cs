using ExpenseTracker.Model;

namespace ExpenseTracker.Services
{
    internal class ExpenseService : IExpenseService
    {
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

                expenseList.Add(expense);
                return expense.Id;
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
