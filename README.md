# FinanceApp

Minimal expense tracker using ASP.NET Core (.NET 8) with Razor views.  
This README covers running the app locally and contribution guidelines.

---

## Quick overview
- Framework: .NET 8
- UI: Razor views (Views/ folder)
- Data: Entity Framework Core with SQL Server (FinanceAppContext)
- Connection string name: `DefaultConnectionString` (in appsettings.json)

---

## Prerequisites
- Visual Studio 2022 (or later) with .NET 8 workload OR .NET 8 SDK
- SQL Server (LocalDB, SQL Express, or full SQL Server)
- Optional: EF Core CLI tools (for migrations)

Install EF CLI (if needed):

---

## Configure (local)
1. Clone the repository:

2. Update connection string in `appsettings.json`:
Replace with your SQL Server details as needed.

---

## Database: migrations & update
If the project contains migrations already:

To create a new migration (when models change):

(Use correct path to the project file if different.)

---

## Run the app
From Visual Studio:
- Open solution, set the web project as startup, then use __Debug > Start Debugging__ (or press __F5__) or __Debug > Start Without Debugging__.

From command line:

By default the app runs on the configured Kestrel port (check console output).

---

## Useful endpoints
- Expenses list: `/Expenses`
- Create Expense: `/Expenses/Create`
- Edit/Delete: `/Expenses/Edit/{id}` and `/Expenses/Delete/{id}`
- Chart data JSON: `/Expenses/GetChart`

---

## Contributing

High-level workflow:
1. Fork the repo and create a feature branch:
   - `git checkout -b feat/short-description`
2. Work in small, focused commits. Use clear messages:
   - `feat(expenses): add inline edit for rows`
   - `fix(validation): show Amount range error`
3. Push branch and open a Pull Request to `main` (or the project's default branch).
4. Ensure PR includes description, testing steps and any DB migration notes.

PR checklist:
- [ ] Code builds locally
- [ ] No runtime errors for the happy path
- [ ] Database migrations included (if schema changes)
- [ ] Unit tests added (if applicable) and passing
- [ ] UI changes are responsive and accessible

Branch naming recommendation:
- feat/..., fix/..., refactor/..., docs/..., test/...

Code review expectations:
- Keep changes small and focused
- Add unit tests where applicable
- Provide a short description and testing steps in the PR

---

## Coding style & tools
- Follow C# conventions (nullable reference types enabled in project if applicable).
- Keep controllers thin: business logic should live in services (see `Data/Service`).
- Use EF Core async APIs (`ToListAsync`, `SaveChangesAsync`).
- Use Visual Studio formatting via __Tools > Options > Text Editor > C# > Code Style__ and running __Edit > Advanced > Format Document__.

Optional checks:
- Run `dotnet format` to apply consistent formatting.
- Run `dotnet build` and `dotnet test` (if tests exist).

---

## Testing
- There are no automated tests in the initial project. Add tests under a test project named `FinanceApp.Tests` and run:

---

## Issues & feature requests
- Open an issue describing the problem, steps to reproduce, expected and actual behavior.
- Tag issues with labels (bug, enhancement, question).

---

## License & Code of Conduct
- Add the repository license file (e.g., `LICENSE`) and a `CODE_OF_CONDUCT.md` if you plan to accept outside contributions.
- Default to a permissive license (MIT) unless otherwise required.

---

If you want, I can:
- Add a CONTRIBUTING.md with a PR template.
- Add a lightweight seed data provider.
- Add a single unit-test project and example tests for the Expenses service.