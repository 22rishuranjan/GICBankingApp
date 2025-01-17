

namespace AwesomeGIC.BankingApp.Tests
{
    [Collection("SequentialTests")]
    public class AppPrintTests : BaseTest
    {
        [Fact]
        public void PrintStatement_ValidAccountAndMonth_PrintsStatement()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appPrint = new AppPrint(loggerMock.Object);

            var accounts = new Dictionary<string, List<Transaction>>
    {
        {
            "TestAccount", new List<Transaction>
            {
                new Transaction(new DateTime(2025, 01, 01), "20250101-01", "D", 500),
                new Transaction(new DateTime(2025, 01, 05), "20250105-01", "W", 200)
            }
        }
    };

            var input = "TestAccount 202501\n"; // Simulate user pressing Enter
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appPrint.PrintStatement(accounts);

            // Assert

            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Account: TestAccount"))), Times.Once);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("| Date     | Txn Id      | Type | Amount | Balance |"))), Times.Once);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("| 20250101 | 20250101-01 | D    | 500.00 |"))), Times.Once);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("| 20250105 | 20250105-01 | W    | 200.00 |"))), Times.Once);
        }

        [Fact]
        public void PrintStatement_InvalidAccount_LogsErrorMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appPrint = new AppPrint(loggerMock.Object);

            var accounts = new Dictionary<string, List<Transaction>>();

            var input = "InvalidAccount 202501";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appPrint.PrintStatement(accounts);

            // Assert
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Account not found."))), Times.Once);
        }

        [Fact]
        public void PrintStatement_InvalidDateFormat_LogsErrorMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appPrint = new AppPrint(loggerMock.Object);

            var accounts = new Dictionary<string, List<Transaction>>();

            var input = "TestAccount InvalidDate";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appPrint.PrintStatement(accounts);

            // Assert
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Invalid input. Please try again."))), Times.Once);
        }

        [Fact]
        public void PrintStatement_NoTransactions_LogsNoTransactionsMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appPrint = new AppPrint(loggerMock.Object);

            var accounts = new Dictionary<string, List<Transaction>>
                {
                    { "TestAccount", new List<Transaction>() } // Empty list of transactions
                };

            var input = "TestAccount 202501\n"; // Simulate user pressing Enter
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appPrint.PrintStatement(accounts);

            // Assert
            // Verify the log message for no transactions
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("No transactions found for the specified month."))), Times.Once);

            // Verify the initial prompt is logged
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Please enter account and month to generate the statement"))), Times.Once);
        }

    }
}