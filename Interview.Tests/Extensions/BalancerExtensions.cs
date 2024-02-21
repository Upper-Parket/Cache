using Interview.Contracts;

namespace Interview.Tests.Extensions;

internal static class BalancerExtensions
{
    public static List<ICache> AddRealShards(this ICacheBalancer balancer, int quantity)
    {
        var caches = new List<ICache>();

        for (var index = 0; index < quantity; index++)
        {
            var cache = new Cache();
            balancer.AddShard(cache);
            caches.Add(cache);
        }

        return caches;
    }
}