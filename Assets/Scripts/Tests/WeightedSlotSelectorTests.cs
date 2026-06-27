using System.Collections.Generic;
using NUnit.Framework;

public class WeightedSlotSelectorTests
{
    private static WeightedSlotSelector WithRng(float value)
    {
        return new WeightedSlotSelector(() => value);
    }

    // weights {10,20,30,40} -> cumulative {10,30,60,100}, total 100

    [Test]
    public void Select_RngZero_ReturnsFirstSlot()
    {
        var weights = new List<float> { 10, 20, 30, 40 };
        Assert.AreEqual(0, WithRng(0f).Select(weights));
    }

    [Test]
    public void Select_RngInSecondBucket_ReturnsSecondSlot()
    {
        var weights = new List<float> { 10, 20, 30, 40 };
        Assert.AreEqual(1, WithRng(0.15f).Select(weights)); // 15 -> (10, 30]
    }

    [Test]
    public void Select_RngInThirdBucket_ReturnsThirdSlot()
    {
        var weights = new List<float> { 10, 20, 30, 40 };
        Assert.AreEqual(2, WithRng(0.5f).Select(weights)); // 50 -> (30, 60]
    }

    [Test]
    public void Select_RngNearMax_ReturnsLastSlot()
    {
        var weights = new List<float> { 10, 20, 30, 40 };
        Assert.AreEqual(3, WithRng(0.95f).Select(weights)); // 95 -> (60, 100]
    }

    [Test]
    public void Select_SingleSlot_AlwaysReturnsZero()
    {
        var weights = new List<float> { 7 };
        Assert.AreEqual(0, WithRng(0f).Select(weights));
        Assert.AreEqual(0, WithRng(1f).Select(weights));
    }

    [Test]
    public void Select_EmptyOrZeroTotal_ReturnsZero()
    {
        Assert.AreEqual(0, WithRng(0.5f).Select(new List<float>()));
        Assert.AreEqual(0, WithRng(0.5f).Select(new List<float> { 0, 0 }));
    }
}
