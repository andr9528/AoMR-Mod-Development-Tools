using System.Reflection;
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
        service = new RelicMultiplierModService(mockDb.Object);
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

    [Test]
    public void FindEffectPairs_ShouldReturnEmpty_WhenNoMatchingTargets()
    {
        // Arrange
        var tech = new Tech
        {
            Effects =
            {
                new Effect
                {
                    MergeMode = MergeMode.ADD,
                    Targets = {new Target {Type = "Unit", Value = "TownCenter",},},
                },
                new Effect
                {
                    MergeMode = MergeMode.REMOVE,
                    Targets = {new Target {Type = "Unit", Value = "Farm",},},
                },
            },
        };

        // Act
        var pairs = InvokeFindEffectPairs(tech);

        // Assert
        pairs.Should().BeEmpty();
    }

    [Test]
    public void FindEffectPairs_ShouldReturnPair_WhenMatchingTargetExists()
    {
        // Arrange
        var addEffect = new Effect
        {
            MergeMode = MergeMode.ADD,
            Targets = {new Target {Type = "Unit", Value = "TownCenter",},},
        };

        var removeEffect = new Effect
        {
            MergeMode = MergeMode.REMOVE,
            Targets = {new Target {Type = "Unit", Value = "TownCenter",},},
        };

        var tech = new Tech {Effects = {addEffect, removeEffect,},};

        // Act
        var pairs = InvokeFindEffectPairs(tech).ToList();

        // Assert
        pairs.Should().HaveCount(1);
        pairs[0].Add.Should().BeSameAs(addEffect);
        pairs[0].Remove.Should().BeSameAs(removeEffect);
    }

    [Test]
    public void FindEffectPairs_ShouldReturnMultiplePairs_WhenMultipleTargetsMatch()
    {
        // Arrange
        var addEffect1 = new Effect
        {
            MergeMode = MergeMode.ADD,
            Targets = {new Target {Type = "Unit", Value = "TownCenter",},},
        };

        var addEffect2 = new Effect
        {
            MergeMode = MergeMode.ADD,
            Targets = {new Target {Type = "Unit", Value = "Farm",},},
        };

        var removeEffect1 = new Effect
        {
            MergeMode = MergeMode.REMOVE,
            Targets = {new Target {Type = "Unit", Value = "TownCenter",},},
        };

        var removeEffect2 = new Effect
        {
            MergeMode = MergeMode.REMOVE,
            Targets = {new Target {Type = "Unit", Value = "Farm",},},
        };

        var tech = new Tech {Effects = {addEffect1, addEffect2, removeEffect1, removeEffect2,},};

        // Act
        var pairs = InvokeFindEffectPairs(tech).ToList();

        // Assert
        pairs.Should().HaveCount(2);
        pairs.Should().ContainSingle(p => p.Add == addEffect1 && p.Remove == removeEffect1);
        pairs.Should().ContainSingle(p => p.Add == addEffect2 && p.Remove == removeEffect2);
    }

    private IEnumerable<(Effect Add, Effect Remove)> InvokeFindEffectPairs(Tech tech)
    {
        MethodInfo? method = typeof(RelicMultiplierModService).GetMethod("FindEffectPairs",
            BindingFlags.NonPublic | BindingFlags.Instance);

        return (IEnumerable<(Effect Add, Effect Remove)>) method.Invoke(service, [tech,]);
    }

    private double InvokeCalc(Relativity relativity, double oldAmount, double multiplier)
    {
        MethodInfo? method = typeof(RelicMultiplierModService).GetMethod("CalculateNewAmount",
            BindingFlags.NonPublic | BindingFlags.Instance);

        return (double) method.Invoke(service, [relativity, oldAmount, multiplier,]);
    }
}
