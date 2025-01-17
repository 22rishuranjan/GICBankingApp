
namespace AwesomeGIC.BankingApp.Core.Interface;

public interface IAppInterest
{
    public void DefineInterestRules(Dictionary<DateTime, InterestRule> interestRules);

    public void PrintInterestRules(Dictionary<DateTime, InterestRule> interestRules);
    
}
