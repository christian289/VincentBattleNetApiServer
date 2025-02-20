using VincentBattleNetApiServer.Core.Databases;
using VincentBattleNetApiServer.Core.DTOs.Base;
using VincentBattleNetApiServer.Core.ServiceInterfaces;

namespace VincentBattleNetApiServer.Core.Services;

public class CacheService(IMemoryCache cache, ILiteDbService liteDbService) : ICacheService
{
    private const int CACHE_EXPIRATION_HOURS = 24;
    private readonly IMemoryCache cache = cache;
    private readonly ILiteDbService liteDbService = liteDbService;

    public IEnumerable<LiteDbValue<T>>? LoadDbToCache<T>(string key) where T : BaseValue
    {
        // 1. ���� ĳ�� Ȯ��
        if (cache.TryGetValue(key, out IEnumerable<LiteDbValue<T>>? cachedValue)) return cachedValue!;

        // 2. DB���� ������ ��ȸ
        var dbCollection = liteDbService.FindAll<LiteDbValue<T>>(key);
        if (!dbCollection.Any()) return null;

        // 3. ������ ���� ���� Ȯ��
        var isExpired = dbCollection.Any(x => (DateTime.Now - x.LastUpdatedTime).TotalHours >= CACHE_EXPIRATION_HOURS);
        if (isExpired)
        {
            liteDbService.DeleteAll<LiteDbValue<T>>(key);

            return null;
        }

        // 4. ��ȿ�� �����͸� ĳ�ÿ� �����ϰ� ��ȯ
        return SetCachingValue(key, dbCollection);
    }

    public List<LiteDbValue<T>> SetCacheAndDatabase<T>(string key, T[] setItems)
    {
        var items = setItems.Select(x => new LiteDbValue<T>(x));
        _ = liteDbService.InsertBulk(key, items.ToArray());

        return SetCachingValue(key, items.ToList());
    }

    public string SetCachingValue(string key, string value)
    {
        cache.Set(key, value);

        return value;
    }

    public T SetCachingValue<T>(string key, T value)
    {
        cache.Set(key, value);

        return value;
    }

    public string SetCachingValue(string key, string value, TimeSpan expiration)
    {
        cache.Set(key, value, expiration);

        return value;
    }

    public T SetCachingValue<T>(string key, T value, TimeSpan expiration)
    {
        cache.Set(key, value, expiration);

        return value;
    }

    public string? GetCachedValue(string key)
    {
        if (!cache.TryGetValue(key, out string? cachedValue))
            cachedValue = string.Empty;

        return cachedValue;
    }

    public T? GetCachedValue<T>(string key)
    {
        if (!cache.TryGetValue(key, out T? cachedValue))
            cachedValue = default;

        return cachedValue;
    }
}