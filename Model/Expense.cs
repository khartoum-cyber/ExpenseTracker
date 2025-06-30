using ExpenseTracker.Enums;

namespace ExpenseTracker.Model
{
    public class Expense
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public Categories Category { get; set; } = Categories.General;

        public DateTime CreatedAt { get; set; }

        public double Amount { get; set; }
    }
}