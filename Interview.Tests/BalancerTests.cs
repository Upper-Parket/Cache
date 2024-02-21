using Interview.Contracts;
using Moq;
using NUnit.Framework;

namespace Interview.Tests;

public class BalancerTests
{
    private static readonly ISeedProvider SeedProvider;

    static BalancerTests()
    {
        var mock = new Mock<ISeedProvider>();
        mock.Setup(x => x.GetSeed)
            .Returns(42);
        SeedProvider = mock.Object;
    }

    #region AddShard

    [Test]
    public void AddShard_ShardIsValid_ShouldAdd()
    {
        var balancer = new Balancer(SeedProvider);
        var cache = new Mock<ICache>().Object;

        void ShouldWork() => balancer.AddShard(cache);

        Assert.DoesNotThrow(ShouldWork);
    }

    [Test]
    public void AddShard_ShardIsNull_ThrowArgumentNullException()
    {
        var balancer = new Balancer(SeedProvider);

        void ShouldThrow() => balancer.AddShard(null);

        Assert.Throws<ArgumentNullException>(ShouldThrow);
    }

    [Test]
    public void AddShard_ShardHaveAlreadyBeenAdded_ThrowArgumentException()
    {
        var balancer = new Balancer(SeedProvider);
        var cache = new Mock<ICache>().Object;
        balancer.AddShard(cache);

        void ShouldThrow() => balancer.AddShard(cache);

        Assert.Throws<ArgumentException>(ShouldThrow);
    }

    #endregion

    #region RemoveShard

    [Test]
    public void RemoveShard_ShardIsNull_ThrowArgumentNullException()
    {
        var balancer = new Balancer(SeedProvider);

        void ShouldThrow() => balancer.RemoveShard(null);

        Assert.Throws<ArgumentNullException>(ShouldThrow);
    }

    [Test]
    public void RemoveShard_ShardWasAdded_ShouldRemove()
    {
        var balancer = new Balancer(SeedProvider);
        var cache = new Mock<ICache>().Object;
        balancer.AddShard(cache);

        void ShouldWork() => balancer.RemoveShard(cache);

        Assert.DoesNotThrow(ShouldWork);
    }

    [Test]
    public void RemoveShard_ShardIsNotInBalancer_ShouldThrowKeyNotFound()
    {
        var balancer = new Balancer(SeedProvider);
        var cache = new Mock<ICache>().Object;

        void ShouldThrow() => balancer.RemoveShard(cache);

        Assert.Throws<KeyNotFoundException>(ShouldThrow);
    }

    #endregion

    #region SetPage

    [Test]
    public void SetPage_NoShardsAdded_ThrowsInvalidOperationException()
    {
        var balancer = new Balancer(SeedProvider);

        void ShouldThrow() => balancer.SetPage("key", "first value");

        Assert.Throws<InvalidOperationException>(ShouldThrow);
    }

    [Test]
    public void SetPage_AllIsCorrect_ShouldSet()
    {
        var balancer = new Balancer(SeedProvider);
        var cacheMock = new Mock<ICache>();
        balancer.AddShard(cacheMock.Object);

        balancer.SetPage("key", "value");

        Assert.Pass();
    }

    [Test]
    public void SetPage_PageExists_ShouldOverwrite()
    {
    }

    #endregion

    #region GetPage

    [Test]
    public void GetPage_PageHasBeenSet_ShouldReturnCorrectValue()
    {
    }

    [Test]
    public void GetPage_PageIsNotInCache_ReturnNull()
    {
    }

    [Test]
    public void GetPage_SetPageAndAddShard_ShouldWorkTheSame()
    {
    }

    #endregion
}