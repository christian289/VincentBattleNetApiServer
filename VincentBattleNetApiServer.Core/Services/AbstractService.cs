using VincentBattleNetApiServer.Core.Options;
using VincentBattleNetApiServer.Core.ServiceInterfaces;

namespace VincentBattleNetApiServer.Core.Services;

public abstract class AbstractService(
    HttpClient httpClient,
    ICacheService cacheService)
{
    protected HttpClient HttpClient => httpClient;
    protected ICacheService CacheService => cacheService;
}
