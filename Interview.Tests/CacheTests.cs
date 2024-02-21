using NUnit.Framework;

namespace Interview.Tests;

public class CacheTests
{
    [Test]
    public void GetPage_PageExists_ReturnPage()
    {
        const string key = "key";
        const string value = "value";
        var cache = new Cache();
        cache.SetPage(key, value);

        var result = cache.GetPage(key);

        Assert.AreEqual(value, result);
    }

    [Test]
    public void GetPage_PageDoesNotExists_ReturnNull()
    {
        var cache = new Cache();

        var result = cache.GetPage("key");

        Assert.IsNull(result);
    }

    [Test]
    public void SetPage_PageDoesNotExist_SetPage()
    {
        var cache = new Cache();

        cache.SetPage("key", "value");

        Assert.Pass();
    }

    [Test]
    public void SetPage_PageExists_OverwritePage()
    {
        const string key = "key";
        const string oldValue = "old value";
        const string newValue = "newValue";
        var cache = new Cache();
        cache.SetPage(key, oldValue);
        cache.SetPage(key, newValue);

        var result = cache.GetPage(key);

        Assert.AreEqual(newValue, result);
    }
}