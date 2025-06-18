namespace ExpenseTracker.Services
{
    internal interface IExpenseService
    {
        int Add(string description, double amount, string category);
    }
}
