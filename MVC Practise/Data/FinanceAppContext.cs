using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data
{
    public class FinanceAppContext :DbContext
    {
        public FinanceAppContext(DbContextOptions<FinanceAppContext> options) : base(options) { }
        public DbSet<FinanceApp.Models.Expense> Expenses { get; set; } = null!;

    }
}
