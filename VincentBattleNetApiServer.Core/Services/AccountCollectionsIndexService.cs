using VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VincentBattleNetApiServer.Core.Services;

public class AccountCollectionsIndexService(
    HttpClient httpClient,
    ICacheService cacheService,
    ILiteDbService liteDbService,
    IBattleNetAccessTokenService accessTokenService) : AbstractService(httpClient, cacheService), IAccountCollectionsIndexService
{
    private ILiteDbService LiteDbService => liteDbService;
    private readonly IBattleNetAccessTokenService accessTokenService = accessTokenService;

    public async Task<ResAccountCollectionsIndex> GetResAccountCollectionsIndexAsync()
    {
        var accessToken = await accessTokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new($"https://{Consts.Region}.api.blizzard.com{Endpoints.AccountCollectionsIndex}")
        {
            Query = new QueryBuilder
            {
                { "namespace", Consts.ProfileNamespace },
                { "locale", Consts.Locale }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var accountcollectionsindex_str = await gameDataResponse.Content.ReadAsStringAsync();
        var accountcollectionsindex = JsonSerializer.Deserialize(accountcollectionsindex_str, SourceGenerationContext.Default.ResAccountCollectionsIndex);

        return CacheService.SetCachingValue(Consts.AccountCollectionsIndex, accountcollectionsindex!);
    }
}