using AwesomeGIC.BankingApp.Core.Interface;
using static AwesomeGICBank.Program;

namespace AwesomeGIC.BankingApp.Core.implementation;

public class AppPrint : IAppPrint
{

    private readonly ILoggerService _loggerService;

    public AppPrint(ILoggerService loggerService)
    {
        _loggerService = loggerService;
    }
    public void PrintStatement(Dictionary<string, List<Transaction>> accounts)
    {
         _loggerService.Log("Please enter account and month to generate the statement <Account> <Year><Month> (or enter blank to go back to main menu):");
        Console.Write("> ");
        var input = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(input)) return;

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2 || !DateTime.TryParseExact(parts[1] + "01", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var monthStart))
        {
             _loggerService.Log("Invalid input. Please try again.");
            return;
        }

        var account = parts[0];

        if (!accounts.ContainsKey(account))
        {
             _loggerService.Log("Account not found.");
            return;
        }

        var accountTransactions = accounts[account]
            .Where(t => t.Date.Year == monthStart.Year && t.Date.Month == monthStart.Month)
            .OrderBy(t => t.Date)
            .ToList();

        if (!accountTransactions.Any())
        {
             _loggerService.Log("No transactions found for the specified month.");
            return;
        }

        decimal balance = 0;
         _loggerService.Log($"Account: {account}");
         _loggerService.Log("| Date     | Txn Id      | Type | Amount | Balance |");

        foreach (var txn in accountTransactions)
        {
            balance += txn.Type == "D" ? txn.Amount : -txn.Amount;
             _loggerService.Log($"| {txn.Date:yyyyMMdd} | {txn.TransactionId,-10} | {txn.Type}    | {txn.Amount,6:F2} | {balance,8:F2} |");
        }
    }
}
