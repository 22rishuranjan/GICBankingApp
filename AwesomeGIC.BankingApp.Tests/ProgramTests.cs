

//namespace AwesomeGICBank.BankingApp.Tests
//{
//    public class ProgramTests
//    {
//        [Fact]
//        public void GenerateTransactionId_ShouldReturnCorrectId()
//        {
//            // Arrange
//            var date = new DateTime(2025, 01, 17);
//            var transactions = new List<Transaction>
//            {
//                new Transaction(date, "20250117-01", "D", 100),
//                new Transaction(date, "20250117-02", "W", 50)
//            };

//            // Act
//            var transactionId = Program.GenerateTransactionId(date, transactions);

//            // Assert
//            Assert.Equal("20250117-03", transactionId);
//        }

//        [Fact]
//        public void InputTransactions_ShouldAddDepositSuccessfully()
//        {
//            // Arrange
//            var account = "TestAccount";
//            Program.accounts[account] = new List<Transaction>();

//            var date = new DateTime(2025, 01, 17);
//            var transaction = new Transaction(date, "20250117-01", "D", 500);

//            // Act
//            Program.accounts[account].Add(transaction);

//            // Assert
//            Assert.Single(Program.accounts[account]);
//            Assert.Equal(500, Program.accounts[account].Sum(t => t.Type == "D" ? t.Amount : -t.Amount));
//        }

//        [Fact]
//        public void InputTransactions_ShouldPreventOverdrawnWithdrawal()
//        {
//            // Arrange
//            var account = "TestAccount";
//            Program.accounts[account] = new List<Transaction>();

//            var deposit = new Transaction(new DateTime(2025, 01, 16), "20250116-01", "D", 100);
//            Program.accounts[account].Add(deposit);

//            var withdrawal = new Transaction(new DateTime(2025, 01, 17), "20250117-01", "W", 200);

//            // Act
//            var balance = Program.accounts[account].Sum(t => t.Type == "D" ? t.Amount : -t.Amount);

//            // Assert
//            Assert.True(balance < withdrawal.Amount, "Withdrawal should not be allowed if it exceeds balance.");
//        }

//        [Fact]
//        public void DefineInterestRules_ShouldAddRuleSuccessfully()
//        {
//            // Arrange
//            var date = new DateTime(2025, 01, 17);
//            var ruleId = "Rule01";
//            var rate = 5.0m;
//            var rule = new InterestRule(date, ruleId, rate);

//            // Act
//            Program.interestRules[date] = rule;

//            // Assert
//            Assert.Single(Program.interestRules);
//            Assert.Equal(rule, Program.interestRules[date]);
//        }

//        [Fact]
//        public void PrintStatement_ShouldReturnCorrectBalance()
//        {
//            // Arrange
//            var account = "TestAccount";
//            Program.accounts[account] = new List<Transaction>
//            {
//                new Transaction(new DateTime(2025, 01, 01), "20250101-01", "D", 500),
//                new Transaction(new DateTime(2025, 01, 05), "20250105-01", "W", 200)
//            };

//            // Act
//            var transactions = Program.accounts[account]
//                .Where(t => t.Date.Year == 2025 && t.Date.Month == 1)
//                .OrderBy(t => t.Date)
//                .ToList();

//            var balance = transactions.Sum(t => t.Type == "D" ? t.Amount : -t.Amount);

//            // Assert
//            Assert.Equal(300, balance);
//        }

//        [Fact]
//        public void GenerateTransactionId_ShouldIncrementCorrectlyWithMultipleTransactionsOnSameDay()
//        {
//            // Arrange
//            var date = new DateTime(2025, 01, 17);
//            var transactions = new List<Transaction>
//            {
//                new Transaction(date, "20250117-01", "D", 100),
//                new Transaction(date, "20250117-02", "W", 50),
//                new Transaction(date, "20250117-03", "D", 200)
//            };

//            // Act
//            var transactionId = Program.GenerateTransactionId(date, transactions);

//            // Assert
//            Assert.Equal("20250117-04", transactionId);
//        }
//    }
//}
