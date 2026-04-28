using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Tools.Abstraction.Enum;
using Tools.Model.Mod;
using Tools.Persistence;
using Tools.Service.Mods.RelicMultiplier;

namespace Tools.Tests.Mod;

[TestFixture]
public class RelicMultiplierModServiceTests
{
    private RelicMultiplierModService service;

    [SetUp]
    public void Setup()
    {
        // Create a mock DbContext (no setup needed for math-only tests)
        var mockDb = new Mock<ToolsDatabaseContext>(new object[]
            {new Microsoft.EntityFrameworkCore.DbContextOptions<ToolsDatabaseContext>(),});
        var loggerMock = new Mock<ILogger<RelicMultiplierModService>>();

        service = new RelicMultiplierModService(mockDb.Object, loggerMock.Object);
    }

    [TestCase(10.0, 5, 50.00)]
    [TestCase(2.5, 3, 7.50)]
    public void CalculateNewAmount_WhenAbsolute_Multiplies(double oldAmount, int multiplier, double expected)
    {
        // Arrange
        var relativity = Relativity.ABSOLUTE;

        // Act
        double result = InvokeCalc(relativity, oldAmount, multiplier);

        // Assert
        result.Should().Be(expected);
    }

    [TestCase(10.0, 5, 50.00)]
    public void CalculateNewAmount_WhenAssign_Multiplies(double oldAmount, int multiplier, double expected)
    {
        // Arrange
        var relativity = Relativity.ASSIGN;

        // Act
        double result = InvokeCalc(relativity, oldAmount, multiplier);

        // Assert
        result.Should().Be(expected);
    }

    [TestCase(0.85, 5, 0.25)]
    [TestCase(0.95, 2, 0.90)]
    [TestCase(0.10, 10, 0.05)] // clamp to 0.05
    public void CalculateNewAmount_WhenPercent_AppliesFormula(double oldAmount, int multiplier, double expected)
    {
        // Arrange
        var relativity = Relativity.PERCENT;

        // Act
        double result = InvokeCalc(relativity, oldAmount, multiplier);

        // Assert
        result.Should().Be(expected);
    }

    [TestCase(1.30, 5, 2.50)]
    [TestCase(1.10, 2, 1.20)]
    public void CalculateNewAmount_WhenBasePercent_AppliesFormula(double oldAmount, int multiplier, double expected)
    {
        // Arrange
        var relativity = Relativity.BASE_PERCENT;

        // Act
        double result = InvokeCalc(relativity, oldAmount, multiplier);

        // Assert
        result.Should().Be(expected);
    }


    private double InvokeCalc(
        Relativity relativity, double oldAmount, int multiplier, TechName? tech = null, int targetDecimals = 2,
        string subtype = "")
    {
        MethodInfo? method = typeof(RelicMultiplierModService).GetMethod(
            RelicMultiplierModService.CALCULATE_NEW_AMOUNT_METHOD_NAME, BindingFlags.NonPublic | BindingFlags.Instance);

        return (double) method!.Invoke(service, [
            tech,
            relativity,
            oldAmount,
            multiplier,
            targetDecimals,
            subtype,
        ])!;
    }
}
