using Interview.Contracts;

namespace Interview;

public class Cache : ICache
{
    private readonly Dictionary<string, string> _cache = new();

    public string? GetPage(string? key)
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));

        return _cache.TryGetValue(key, out var value)
            ? value
            : null;
    }

    public void SetPage(string key, string value)
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));

        _cache[key] = value;
    }

    // for debug purposes
    public override string ToString()
    {
        return $"Cache count = {_cache.Count}";
    }
}