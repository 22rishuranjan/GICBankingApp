

namespace AwesomeGIC.BankingApp.Core.Interface;

public interface IAppPrint
{
    internal void PrintStatement(Dictionary<string, List<Transaction>> accounts);
   
}
