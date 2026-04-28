using System.Reflection;
using Microsoft.Extensions.Logging;
using Moq;
using Tools.Abstraction.Enum;
using Tools.Model.Mod;
using Tools.Persistence;
using Tools.Service.Mods.RelicMultiplier;
using Tools.Service.Xml;

namespace Tools.Tests.Mod;

[TestFixture]
public class TechTreeExportServiceTests
{
    private TechTreeExportService service;

    [SetUp]
    public void Setup()
    {
        // Create a mock DbContext (no setup needed for math-only tests)
        var mockDb = new Mock<ToolsDatabaseContext>(new object[]
            {new Microsoft.EntityFrameworkCore.DbContextOptions<ToolsDatabaseContext>(),});

        service = new TechTreeExportService(mockDb.Object);
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
        MethodInfo? method = typeof(TechTreeExportService).GetMethod(
            TechTreeExportService.FIND_EFFECT_PAIRS_METHOD_NAME, BindingFlags.NonPublic | BindingFlags.Instance);

        return (IEnumerable<(Effect Add, Effect Remove)>) method.Invoke(service, [tech,]);
    }
}
