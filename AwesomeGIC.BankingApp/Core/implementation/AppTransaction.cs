using AwesomeGIC.BankingApp.Core.Interface;
using static AwesomeGICBank.Program;

namespace AwesomeGIC.BankingApp.Core.implementation;

public class AppTransaction : IAppTransaction
{
    private readonly ILoggerService _loggerService;

    public AppTransaction(ILoggerService loggerService)
    {
        _loggerService = loggerService;
    }
    public void InputTransactions(Dictionary<string, List<Transaction>> accounts)
    {
        while (true)
        {
            _loggerService.Log("Please enter transaction details in <Date> <Account> <Type> <Amount> format (or enter blank to go back to main menu):");
            _loggerService.Log("> ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input)) return;

            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 4 || !DateTime.TryParseExact(parts[0], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) ||
                !decimal.TryParse(parts[3], out var amount) || amount <= 0)
            {
                _loggerService.Log("Invalid input. Please try again.");
                continue;
            }

            var account = parts[1];
            var type = parts[2].ToUpper();

            if (type != "D" && type != "W")
            {
                     _loggerService.Log("Invalid transaction type. Use 'D' for deposit or 'W' for withdrawal.");
                continue;
            }

            if (!accounts.ContainsKey(account)) accounts[account] = new List<Transaction>();

            var accountTransactions = accounts[account];
            var balance = accountTransactions.Sum(t => t.Type == "D" ? t.Amount : -t.Amount);

            if (type == "W" && balance - amount < 0)
            {
                     _loggerService.Log("Insufficient balance for this transaction.");
                continue;
            }

            var transactionId = GenerateTransactionId(date, accountTransactions);
            accountTransactions.Add(new Transaction(date, transactionId, type, amount));
                 _loggerService.Log($"Transaction added successfully. Current balance: {balance + (type == "D" ? amount : -amount):F2}");
        }
    }

    public string GenerateTransactionId(DateTime date, List<Transaction> transactions)
    {
        var count = transactions.Count(t => t.Date == date) + 1;
        return $"{date:yyyyMMdd}-{count:D2}";
    }
}
