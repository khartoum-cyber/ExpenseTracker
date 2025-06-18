namespace ExpenseTracker.Services
{
    internal interface IExpenseService
    {
        string Add(string description, double amount);
    }
}
