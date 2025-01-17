using AwesomeGIC.BankingApp.Core.Interface;
using static AwesomeGICBank.Program;

namespace AwesomeGIC.BankingApp.Core.implementation;

public class AppInterest : IAppInterest
{

    private readonly ILoggerService _loggerService;

    public AppInterest(ILoggerService loggerService)
    {
        _loggerService = loggerService;
    }
    public void DefineInterestRules(Dictionary<DateTime, InterestRule> interestRules)
    {
        while (true)
        {
             _loggerService.Log("Please enter interest rules details in <Date> <RuleId> <Rate in %> format (or enter blank to go back to main menu):");
            Console.Write("> ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input)) return;

            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3 || !DateTime.TryParseExact(parts[0], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) ||
                !decimal.TryParse(parts[2], out var rate) || rate <= 0 || rate >= 100)
            {
                 _loggerService.Log("Invalid input. Please try again.");
                continue;
            }

            var ruleId = parts[1];
            interestRules[date] = new InterestRule(date, ruleId, rate);

             _loggerService.Log("Interest rule added successfully.");
            PrintInterestRules(interestRules);
        }
    }

    public void PrintInterestRules(Dictionary<DateTime, InterestRule> interestRules)
    {
         _loggerService.Log("Interest rules:");
         _loggerService.Log("| Date     | RuleId | Rate (%) |");
        foreach (var rule in interestRules.OrderBy(r => r.Key))
        {
             _loggerService.Log($"| {rule.Key:yyyyMMdd} | {rule.Value.RuleId,-6} | {rule.Value.Rate,8:F2} |");
        }
    }
}
