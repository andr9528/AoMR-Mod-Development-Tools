using System.Reflection;
using Tools.Abstraction.Enum;
using Tools.Service;

namespace Tools.Tests.Mod;

[TestFixture]
public class RelicModServiceTests
{
    private RelicModService _service;

    [SetUp]
    public void Setup()
    {
        // We don’t need a DbContext for these tests, so just create an uninitialized object
        _service =
            (RelicModService) System.Runtime.Serialization.FormatterServices.GetUninitializedObject(
                typeof(RelicModService));
    }

    [TestCase(10.0, 5, 50.00)]
    [TestCase(2.5, 3, 7.50)]
    public void CalculateNewAmount_WhenAbsolute_Multiplies(double oldAmount, double multiplier, double expected)
    {
        // Arrange
        var relativity = Relativity.ABSOLUTE;

        // Act
        double result = InvokeCalc(relativity, oldAmount, multiplier);

        // Assert
        result.Should().Be(expected);
    }

    [TestCase(10.0, 5, 50.00)]
    public void CalculateNewAmount_WhenAssign_Multiplies(double oldAmount, double multiplier, double expected)
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
    public void CalculateNewAmount_WhenPercent_AppliesFormula(double oldAmount, double multiplier, double expected)
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
    public void CalculateNewAmount_WhenBasePercent_AppliesFormula(double oldAmount, double multiplier, double expected)
    {
        // Arrange
        var relativity = Relativity.BASE_PERCENT;

        // Act
        double result = InvokeCalc(relativity, oldAmount, multiplier);

        // Assert
        result.Should().Be(expected);
    }

    private double InvokeCalc(Relativity relativity, double oldAmount, double multiplier)
    {
        MethodInfo? method = typeof(RelicModService).GetMethod("CalculateNewAmount",
            BindingFlags.NonPublic | BindingFlags.Instance);

        return (double) method.Invoke(_service, new object[] {relativity, oldAmount, multiplier,});
    }
}
