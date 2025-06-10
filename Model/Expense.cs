namespace ExpenseTracker.Model
{
    internal class Expense
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public double Amount { get; set; }
    }
}
