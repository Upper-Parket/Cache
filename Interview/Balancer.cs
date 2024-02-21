using Interview.Contracts;
using Interview.Extensions;

namespace Interview;

// it is an interface and not an integer because we might want to create 
// a Balancer instance via container
// with an integer in the constructor this might be a problem
public class Balancer(ISeedProvider seedProvider) : ICache, ICacheBalancer
{
    // System.Random is not thread-safe and not cryptographically secure
    // it is possible to replace it with an abstraction that would have any implementation
    // that would suit the purpose of this class
    private readonly Random _random = new(seedProvider.GetSeed);
    private readonly SortedDictionary<int, ICache> _cacheMap = new();
    private readonly Dictionary<ICache, int> _cacheToKeyMap = new();

    public string? GetPage(string key)
    {
        var id = GetCacheId(key);
        var shard = _cacheMap[id];
        return shard.GetPage(key);
    }

    public void SetPage(string key, string value)
    {
        var id = GetCacheId(key);
        var shard = _cacheMap[id];
        shard.SetPage(key, value);
    }

    private int GetCacheId(string key)
    {
        if (_cacheMap.Count == 0)
            throw new InvalidOperationException("No shards have been added");

        var absHashCode = Math.Abs(key.GetHashCode());
        var cacheKvp = _cacheMap.FirstOrDefault(kvp => kvp.Key > absHashCode);
        return cacheKvp.IsDefault() ? _cacheMap.First().Key : cacheKvp.Key;
    }

    public void AddShard(ICache cache)
    {
        if (cache is null)
            throw new ArgumentNullException(nameof(cache));

        if (_cacheToKeyMap.ContainsKey(cache))
            throw new ArgumentException("An element with the same key already exists", nameof(cache));

        int randomValue;
        do
        {
            randomValue = _random.Next();
        } while (_cacheMap.ContainsKey(randomValue));
        _cacheMap.Add(randomValue, cache);
        _cacheToKeyMap.Add(cache, randomValue);
    }

    public void RemoveShard(ICache cache)
    {
        if (cache is null)
            throw new ArgumentNullException(nameof(cache));

        if (!_cacheToKeyMap.Remove(cache, out var key))
            throw new KeyNotFoundException();
        if (!_cacheMap.Remove(key))
            throw new KeyNotFoundException();
    }
}