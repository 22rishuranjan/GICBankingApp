namespace AwesomeGIC.BankingApp.Core.Interface;

public interface IAppTransaction
{
    public void InputTransactions(Dictionary<string, List<Transaction>> accounts);

    public string GenerateTransactionId(DateTime date, List<Transaction> transactions);
    
}
