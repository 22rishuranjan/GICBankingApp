
using AwesomeGIC.BankingApp;
using AwesomeGIC.BankingApp.Core.implementation;
using AwesomeGIC.BankingApp.Core.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank;

public class Program
{
    public static Dictionary<string, List<Transaction>> accounts = new();
    public static Dictionary<DateTime, InterestRule> interestRules = new();

    public static void Main(string[] args)
    {

        // Configure the DI container
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IAppTransaction, AppTransaction>()
            .AddSingleton<IAppPrint, AppPrint>()
            .AddSingleton<IAppInterest, AppInterest>()
            .AddSingleton<ILoggerService, LoggerService>()
            .AddSingleton<BankingApp>()
            .BuildServiceProvider();

        // Resolve the root service (or run application logic)
        var app = serviceProvider.GetRequiredService<BankingApp>();
        app.Run();
    }

    public interface ILoggerService
    { 
        void Log(string message);
    }

    public class LoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine($"[Log] {message}");
        }
    }
}
