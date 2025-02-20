
using VincentBattleNetApiServer.Core.Databases;
using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.ServiceInterfaces;

public interface ICacheService
{
    string SetCachingValue(string key, string value);

    T SetCachingValue<T>(string key, T value);

    string SetCachingValue(string key, string value, TimeSpan expiration);

    T SetCachingValue<T>(string key, T value, TimeSpan expiration);

    string? GetCachedValue(string key);

    IEnumerable<LiteDbValue<T>>? LoadDbToCache<T>(string key) where T : BaseValue;

    List<LiteDbValue<T>> SetCacheAndDatabase<T>(string key, T[] setItems);
}