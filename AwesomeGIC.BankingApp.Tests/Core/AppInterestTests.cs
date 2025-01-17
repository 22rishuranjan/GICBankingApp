

namespace AwesomeGIC.BankingApp.Tests
{
    [Collection("SequentialTests")]
    public class AppInterestTests : BaseTest
    {
        [Fact]
        public void DefineInterestRules_ValidInput_AddsRuleSuccessfully()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appInterest = new AppInterest(loggerMock.Object);

            var interestRules = new Dictionary<DateTime, InterestRule>();
            var input = "20250101 Rule01 5.0";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appInterest.DefineInterestRules(interestRules);

            // Assert
            Assert.Single(interestRules);
            var rule = interestRules[new DateTime(2025, 01, 01)];
            Assert.Equal("Rule01", rule.RuleId);
            Assert.Equal(5.0m, rule.Rate);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Interest rule added successfully."))), Times.Once);
        }

        [Fact]
        public void DefineInterestRules_InvalidInput_LogsErrorMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appInterest = new AppInterest(loggerMock.Object);

            var interestRules = new Dictionary<DateTime, InterestRule>();
            var input = "InvalidInput";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appInterest.DefineInterestRules(interestRules);

            // Assert
            Assert.Empty(interestRules);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Invalid input. Please try again."))), Times.Once);
        }

        [Fact]
        public void DefineInterestRules_BlankInput_ExitsGracefully()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appInterest = new AppInterest(loggerMock.Object);

            var interestRules = new Dictionary<DateTime, InterestRule>();
            var input = "\n"; // Simulate a blank input (user pressing enter)
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            appInterest.DefineInterestRules(interestRules);

            // Assert
            Assert.Empty(interestRules);

            // Verify the initial prompt is logged
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Please enter interest rules details in <Date> <RuleId> <Rate in %> format (or enter blank to go back to main menu):"))), Times.Once);

            // Verify no additional calls
            loggerMock.VerifyNoOtherCalls();
        }


        [Fact]
        public void PrintInterestRules_ValidInterestRules_LogsCorrectOutput()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appInterest = new AppInterest(loggerMock.Object);

            var interestRules = new Dictionary<DateTime, InterestRule>
            {
                { new DateTime(2025, 01, 01), new InterestRule(new DateTime(2025, 01, 01), "Rule01", 5.0m) },
                { new DateTime(2025, 01, 15), new InterestRule(new DateTime(2025, 01, 15), "Rule02", 3.5m) }
            };

            // Act
            appInterest.PrintInterestRules(interestRules);

            // Assert
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("| 20250101 | Rule01 |     5.00 |"))), Times.Once);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("| 20250115 | Rule02 |     3.50 |"))), Times.Once);
        }

        [Fact]
        public void PrintInterestRules_EmptyInterestRules_LogsNoRulesMessage()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var appInterest = new AppInterest(loggerMock.Object);

            var interestRules = new Dictionary<DateTime, InterestRule>();

            // Act
            appInterest.PrintInterestRules(interestRules);

            // Assert
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Interest rules:"))), Times.Once);
            loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("| Date     | RuleId | Rate (%) |"))), Times.Once);
            loggerMock.VerifyNoOtherCalls();
        }
    }
}