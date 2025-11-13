using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinanceApp.Models;
using FinanceApp.Data.Service;

namespace FinanceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExpensesService _expensesService;

        public HomeController(ILogger<HomeController> logger, IExpensesService expensesService)
        {
            _logger = logger;
            _expensesService = expensesService;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = (await _expensesService.GetAll()).ToList();

            var total = expenses.Sum(e => e.Amount);
            var thisMonth = expenses
                .Where(e => e.Date.Year == DateTime.Now.Year && e.Date.Month == DateTime.Now.Month)
                .Sum(e => e.Amount);

            var recent = expenses
                .OrderByDescending(e => e.Date)
                .ThenByDescending(e => e.ID)
                .Take(6);

            var categories = expenses
                .GroupBy(e => e.Category)
                .Select(g => new CategorySummary { Category = g.Key, Total = g.Sum(x => x.Amount) })
                .OrderByDescending(c => c.Total);

            var vm = new DashboardViewModel
            {
                TotalExpenses = total,
                ThisMonthTotal = thisMonth,
                RecentExpenses = recent,
                CategorySummaries = categories
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}