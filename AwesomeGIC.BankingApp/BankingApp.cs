using AwesomeGIC.BankingApp.Core.implementation;
using AwesomeGIC.BankingApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AwesomeGICBank.Program;

namespace AwesomeGIC.BankingApp
{
    internal class BankingApp
    {
        private readonly IAppInterest _appInterest;
        private readonly IAppPrint _appPrint;
        private readonly IAppTransaction _appTransaction;
        private readonly ILoggerService _loggerService;

        public BankingApp(IAppInterest appInterest, IAppPrint appPrint, IAppTransaction appTransaction, ILoggerService loggerService)
        {
            _appInterest = appInterest;
            _appPrint = appPrint;
            _appTransaction = appTransaction;
            _loggerService = loggerService;
        }

        public void Run()
        {

            while (true)
            {
                _loggerService.Log("Application started.");
                _loggerService.Log("Welcome to AwesomeGIC Bank! What would you like to do?");
                _loggerService.Log("[T] Input transactions");
                _loggerService.Log("[I] Define interest rules");
                _loggerService.Log("[P] Print statement");
                _loggerService.Log("[Q] Quit");
                Console.Write("> ");

                var choice = Console.ReadLine()?.Trim().ToUpper();

                switch (choice)
                {
                    case "T":
                        
                        _appTransaction.InputTransactions(accounts);
                        break;
                    case "I":
                     
                       _appInterest.DefineInterestRules(interestRules);
                        break;
                    case "P":
                     
                        _appPrint.PrintStatement(accounts);
                        break;
                    case "Q":
                        _loggerService.Log("Thank you for banking with AwesomeGIC Bank. Have a nice day!");
                        _loggerService.Log("Application ended.");
                        return;
                    default:
                        _loggerService.Log("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
