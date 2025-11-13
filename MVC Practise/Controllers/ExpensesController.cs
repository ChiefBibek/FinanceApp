using FinanceApp.Data;
using FinanceApp.Data.Service;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        public ExpensesController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }

        public async Task<IActionResult> Index()
        {
            // Example: read a session filter and a theme cookie
            var filter = HttpContext.Session.GetString("expenses.filter") ?? string.Empty;
            var theme = Request.Cookies["theme"] ?? "light";

            var expenses = await _expensesService.GetAll();

            // Optionally apply a very simple filter (by category) when set in session
            if (!string.IsNullOrEmpty(filter))
            {
                expenses = expenses.Where(e => e.Category?.Equals(filter, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            // Make theme available to the view via ViewData
            ViewData["Theme"] = theme;

            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.Add(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public IActionResult GetChart()
        {
            var data = _expensesService.GetChartData();
            return Json(data);
        }

        // Edit GET
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expensesService.GetById(id);
            if (expense == null) return NotFound();
            return View(expense);
        }

        // Edit POST
        [HttpPost]
        public async Task<IActionResult> Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.Update(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // Delete confirmation GET
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _expensesService.GetById(id);
            if (expense == null) return NotFound();
            return View(expense);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _expensesService.Delete(id);
            return RedirectToAction("Index");
        }

        //
        // Cookie examples
        //
        // Set a simple cookie (theme preference)
        [HttpGet]
        public IActionResult SetTheme(string theme = "light")
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                HttpOnly = false, // allow client-side access if you want to toggle UI client-side
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                IsEssential = true
            };
            Response.Cookies.Append("theme", theme, cookieOptions);
            return RedirectToAction("Index");
        }

        // Delete theme cookie
        [HttpGet]
        public IActionResult ClearTheme()
        {
            Response.Cookies.Delete("theme");
            return RedirectToAction("Index");
        }

        //
        // Session examples
        //
        // Set a simple session value (filter by category)
        [HttpPost]
        public IActionResult SetFilter(string? category)
        {
            if (string.IsNullOrEmpty(category))
            {
                HttpContext.Session.Remove("expenses.filter");
            }
            else
            {
                HttpContext.Session.SetString("expenses.filter", category);
            }
            return RedirectToAction("Index");
        }

        // Read session value (demo endpoint)
        [HttpGet]
        public IActionResult GetFilter()
        {
            var filter = HttpContext.Session.GetString("expenses.filter") ?? string.Empty;
            return Content($"Current session filter: {filter}");
        }

        // Clear session keys for expenses
        [HttpGet]
        public IActionResult ClearFilter()
        {
            HttpContext.Session.Remove("expenses.filter");
            return RedirectToAction("Index");
        }
    }
}