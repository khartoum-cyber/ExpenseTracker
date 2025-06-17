namespace ExpenseTracker.Services
{
    internal interface IExpenseService
    {
        string Add(string description, int amount);
    }
}
