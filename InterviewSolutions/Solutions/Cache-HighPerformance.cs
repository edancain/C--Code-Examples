// High-performance caching system
public class HighPerformanceCache<TKey, TValue>
{
    private readonly ConcurrentDictionary<TKey, CacheItem<TValue>> _cache;
    private readonly ReaderWriterLockSlim _cleanupLock;

    public HighPerformanceCache()
    {
        _cache = new ConcurrentDictionary<TKey, CacheItem<TValue>>();
        _cleanupLock = new ReaderWriterLockSlim();
    }

    public async Task<TValue> GetOrAddAsync(
        TKey key,
        Func<Task<TValue>> valueFactory,
        TimeSpan expiration)
    {
        if (_cache.TryGetValue(key, out var item) && !item.IsExpired)
        {
            return item.Value;
        }

        var value = await valueFactory();
        _cache.AddOrUpdate(key,
            new CacheItem<TValue>(value, expiration),
            (_, __) => new CacheItem<TValue>(value, expiration));

        return value;
    }
}

