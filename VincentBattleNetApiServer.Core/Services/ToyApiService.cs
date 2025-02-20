using VincentBattleNetApiServer.Core.Databases;
using VincentBattleNetApiServer.Core.DTOs.Base;
using VincentBattleNetApiServer.Core.DTOs.Res.ToyAPI;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VincentBattleNetApiServer.Core.Services;

public class ToyApiService(
    HttpClient httpClient,
    ICacheService cacheService,
    IBattleNetAccessTokenService accessTokenService) : AbstractService(httpClient, cacheService), IToyApiService
{
    private readonly IBattleNetAccessTokenService accessTokenService = accessTokenService;

    public async Task<IEnumerable<LiteDbValue<BaseValue>>> GetToyIndexAsync()
    {
        // 캐시/DB 계층에서 데이터 조회
        IEnumerable<LiteDbValue<BaseValue>>? cachedData = CacheService.LoadDbToCache<BaseValue>(Consts.ToyIndex);

        if (cachedData is not null || cachedData!.Count() >= 0)
            return cachedData!;

        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.com{Endpoints.ToyIndex}")
        {
            Query = new QueryBuilder
            {
                { "namespace", Consts.StaticNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var toyindex_str = await gameDataResponse.Content.ReadAsStringAsync();
        var toyindex = JsonSerializer.Deserialize(toyindex_str, SourceGenerationContext.Default.ResToyIndex);

        return CacheService.SetCacheAndDatabase(Consts.ToyIndex, toyindex!.Toys!);
    }

    public async Task<ResToy> GetToyAsync(int toyId)
    {
        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.com{string.Format(Endpoints.Toy, toyId)}")
        {
            Query = new QueryBuilder
            {
                { "toyId", $"{toyId}"},
                { "namespace", Consts.StaticNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var toy_str = await gameDataResponse.Content.ReadAsStringAsync();
        var toy = JsonSerializer.Deserialize(toy_str, SourceGenerationContext.Default.ResToy);

        return toy!;
    }
}
