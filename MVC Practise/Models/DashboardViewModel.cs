using System.Collections.Generic;

namespace FinanceApp.Models
{
    public class CategorySummary
    {
        public string Category { get; set; } = null!;
        public decimal Total { get; set; }
    }

    public class DashboardViewModel
    {
        public decimal TotalExpenses { get; set; }
        public decimal ThisMonthTotal { get; set; }
        public IEnumerable<Expense> RecentExpenses { get; set; } = Enumerable.Empty<Expense>();
        public IEnumerable<CategorySummary> CategorySummaries { get; set; } = Enumerable.Empty<CategorySummary>();
    }
}