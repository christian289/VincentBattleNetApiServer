using VincentBattleNetApiServer.Core.DTOs.Res.RealmAPI;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VincentBattleNetApiServer.Core.Services;

public class RealmApiService(
    HttpClient httpClient,
    ICacheService cacheService,
    ILiteDbService liteDbService,
    IBattleNetAccessTokenService accessTokenService) : AbstractService(httpClient, cacheService), IRealmApiService
{
    private readonly ILiteDbService liteDbService = liteDbService;
    private readonly IBattleNetAccessTokenService accessTokenService = accessTokenService;

    public async Task<ResRealmsIndex> GetRealmsIndexAsync()
    {
        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.com{Endpoints.RealmsIndex}")
        {
            Query = new QueryBuilder
            {
                { "namespace", Consts.DynamicNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var realmindex_str = await gameDataResponse.Content.ReadAsStringAsync();
        var realmindex = JsonSerializer.Deserialize(realmindex_str, SourceGenerationContext.Default.ResRealmsIndex);

        return CacheService.SetCachingValue(Consts.RealmIndex, realmindex!);
    }

    public async Task<ResRealm> GetRealmAsync(string realmSlug)
    {
        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.com{string.Format(Endpoints.Realm, realmSlug)}")
        {
            Query = new QueryBuilder
            {
                { "realmSlug", realmSlug },
                { "namespace", Consts.DynamicNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var realm_str = await gameDataResponse.Content.ReadAsStringAsync();
        var realm = JsonSerializer.Deserialize(realm_str, SourceGenerationContext.Default.ResRealm);

        return realm!;
    }
}