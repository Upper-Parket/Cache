namespace Interview.Extensions
{
    internal static class KeyValuePairExtensions
    {
        public static bool IsDefault<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair) =>
            keyValuePair.Equals(default(KeyValuePair<TKey, TValue>));
    }
}