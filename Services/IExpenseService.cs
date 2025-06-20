using ExpenseTracker.Model;

namespace ExpenseTracker.Services
{
    internal interface IExpenseService
    {
        int Add(string description, double amount, string category);
        //List<Expense> GetAllExpenses();
    }
}
