using VincentBattleNetApiServer.Core.DTOs.Res.WowTokenAPI;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VincentBattleNetApiServer.Core.Services;

public class WowTokenApiService(
    HttpClient httpClient,
    ICacheService cacheService,
    IBattleNetAccessTokenService accessTokenService) : AbstractService(httpClient, cacheService), IWowTokenApiService
{
    private readonly IBattleNetAccessTokenService accessTokenService = accessTokenService;

    public async Task<ResWowTokenIndex> GetWowTokenIndexAsync()
    {
        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.com{Endpoints.WowTokenIndex}")
        {
            Query = new QueryBuilder
            {
                { "namespace", Consts.DynamicNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var tokenindex_str = await gameDataResponse.Content.ReadAsStringAsync();
        var tokenindex = JsonSerializer.Deserialize(tokenindex_str, SourceGenerationContext.Default.ResWowTokenIndex);

        return CacheService.SetCachingValue(Consts.TokenIndex, tokenindex!);
    }
}
