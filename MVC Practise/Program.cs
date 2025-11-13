using FinanceApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Enable HttpContextAccessor (optional but useful)
builder.Services.AddHttpContextAccessor();

// Add distributed memory cache + session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".FinanceApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<FinanceAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnectionString' not found.")));

builder.Services.AddScoped<FinanceApp.Data.Service.IExpensesService, FinanceApp.Data.Service.ExpensesService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANT: session must be registered before UseAuthorization and after UseRouting
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();