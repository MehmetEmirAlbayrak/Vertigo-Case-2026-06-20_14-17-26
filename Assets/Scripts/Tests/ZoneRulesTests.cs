using NUnit.Framework;

public class ZoneRulesTests
{
    [Test]
    public void Zone1_IsBronze()
    {
        Assert.AreEqual(WheelTier.Bronze, ZoneRules.GetWheelTierForZone(1));
    }

    [TestCase(5)]
    [TestCase(10)]
    [TestCase(25)]
    [TestCase(35)]
    public void EveryFifthZone_IsSilver(int zone)
    {
        Assert.AreEqual(WheelTier.Silver, ZoneRules.GetWheelTierForZone(zone));
    }

    [TestCase(30)]
    [TestCase(60)]
    [TestCase(90)]
    public void EveryThirtiethZone_IsGolden(int zone)
    {
        Assert.AreEqual(WheelTier.Golden, ZoneRules.GetWheelTierForZone(zone));
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(29)]
    [TestCase(31)]
    public void NonMultipleZones_AreBronze(int zone)
    {
        Assert.AreEqual(WheelTier.Bronze, ZoneRules.GetWheelTierForZone(zone));
    }

    [Test]
    public void Zone30_GoldenTakesPrecedenceOverSilver()
    {
        // 30 is divisible by both 5 and 30 -> Golden must win (rule order)
        Assert.AreEqual(WheelTier.Golden, ZoneRules.GetWheelTierForZone(30));
    }
}
