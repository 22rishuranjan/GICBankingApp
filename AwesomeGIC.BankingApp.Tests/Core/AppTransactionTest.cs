
namespace AwesomeGIC.BankingApp.Tests
{
    [Collection("SequentialTests")]
    public class AppTransactionTests : BaseTest
    {
        [Fact]
        public void InputTransactions_ValidDeposit_AddsTransactionSuccessfully()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appTransaction = new AppTransaction(loggerMock.Object);

            var accounts = new Dictionary<string, List<Transaction>>();
            var input = "20250101 TestAccount D 500";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appTransaction.InputTransactions(accounts);

            // Assert
            Assert.Single(accounts);
            Assert.Single(accounts["TestAccount"]);
            var transaction = accounts["TestAccount"].First();
            Assert.Equal("D", transaction.Type);
            Assert.Equal(500, transaction.Amount);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Transaction added successfully."))), Times.Once);
        }

        [Fact]
        public void InputTransactions_InvalidInput_LogsErrorMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appTransaction = new AppTransaction(loggerMock.Object);

            var accounts = new Dictionary<string, List<Transaction>>();
            var input = "InvalidInput\n"; // Simulate pressing "Enter"
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appTransaction.InputTransactions(accounts);

            // Assert
            Assert.Empty(accounts);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Invalid input. Please try again."))), Times.Once);
        }

        [Fact]
        public void InputTransactions_InsufficientBalance_LogsErrorMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appTransaction = new AppTransaction(loggerMock.Object);

            var accounts = new Dictionary<string, List<Transaction>>
            {
                { "TestAccount", new List<Transaction> { new Transaction(new DateTime(2025, 01, 01), "20250101-01", "D", 100) } }
            };
            var input = "20250102 TestAccount W 200";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appTransaction.InputTransactions(accounts);

            // Assert
            Assert.Single(accounts["TestAccount"]);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Insufficient balance for this transaction."))), Times.Once);
        }

        [Fact]
        public void GenerateTransactionId_ValidInput_ReturnsCorrectId()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appTransaction = new AppTransaction(loggerMock.Object);

            var transactions = new List<Transaction>
            {
                new Transaction(new DateTime(2025, 01, 01), "20250101-01", "D", 100),
                new Transaction(new DateTime(2025, 01, 01), "20250101-02", "D", 200)
            };

            // Act
            var transactionId = appTransaction.GenerateTransactionId(new DateTime(2025, 01, 01), transactions);

            // Assert
            Assert.Equal("20250101-03", transactionId);
        }

        [Fact]
        public void InputTransactions_InvalidTransactionType_LogsErrorMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appTransaction = new AppTransaction(loggerMock.Object);

            var accounts = new Dictionary<string, List<Transaction>>();
            var input = "20250101 TestAccount X 100";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appTransaction.InputTransactions(accounts);

            // Assert
            Assert.Empty(accounts);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Invalid transaction type."))), Times.Once);
        }
    }
}
