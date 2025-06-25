using ExpenseTracker.Model;

namespace ExpenseTracker.Services
{
    internal interface IExpenseService
    {
        int Add(string description, double amount, string category);
        List<Expense> ListAllExpenses();
        int UpdateExpense(string description, int id, double amount);
        int DeleteExpense(int id);
        double SumAll();
        double SumMonth(int month);
    }
}
